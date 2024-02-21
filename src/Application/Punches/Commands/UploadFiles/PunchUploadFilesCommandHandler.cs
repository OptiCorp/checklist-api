using Application.Punches;
using MediatR;
using MobDeMob.Application.Common.Interfaces;

namespace Application.Upload;

public class PunchUploadFileCommandHandler : IRequestHandler<PunchUploadFilesCommand>
{
    private readonly IFileStorageRepository _fileStorageRepository;
    private readonly ICacheRepository _cacheRepository;
    private readonly IPunchRepository _punchRepository;


    public PunchUploadFileCommandHandler(IFileStorageRepository fileStorageRepository, ICacheRepository cachRepository, IPunchRepository punchRepository)
    {
        _fileStorageRepository = fileStorageRepository;
        _cacheRepository = cachRepository;
        _punchRepository = punchRepository;
    }

    public async Task Handle(PunchUploadFilesCommand request, CancellationToken cancellationToken)
    {
        var punch = await _punchRepository.GetPunch(request.Id, cancellationToken) 
            ?? throw new Exception($"Punch with id: '{request.Id}' does not exist");

        var checklistId = punch.ChecklistItem.ChecklistId.ToString();

        //upload file, close file, add image uri to ImageBlobUris
        foreach(var file in request.Files)
        {
            var blobUri = await _fileStorageRepository.UploadImage(file.Stream, file.FileName, checklistId, file.ContentType, cancellationToken);
            file.Stream.Close();
            punch.ImageBlobUris.Add(blobUri);
        }

        //generate SASToke and set the key-value (checklistId: SAStoken)
        var containerSAS = _cacheRepository.GetValue(punch.ChecklistItem.ChecklistId.ToString());
        if (containerSAS == null)
        {
            var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(checklistId, cancellationToken);
            _cacheRepository.SetKeyValue(checklistId, newContainerSAS, TimeSpan.FromHours(1));
        }

        await _punchRepository.SaveChanges(cancellationToken);
    }
}
