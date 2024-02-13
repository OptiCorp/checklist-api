using Application.Punches;
using MediatR;
using MobDeMob.Application.Common.Interfaces;

namespace Application.Upload;

public class PunchUploadFileCommandHandler : IRequestHandler<PunchUploadFileCommand>
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

    public async Task Handle(PunchUploadFileCommand request, CancellationToken cancellationToken)
    {
        var punch = await _punchRepository.GetPunch(request.Id, cancellationToken) ?? throw new Exception($"Punch with id: '{request.Id}' does not exist");

        var checklistId = punch.ChecklistItem.ChecklistId.ToString();

        var blobUri = await _fileStorageRepository.UploadImage(request.Stream, request.FileName, checklistId, request.ContentType, cancellationToken);
        var containerSAS = _cacheRepository.GetValue(punch.ChecklistItem.ChecklistId.ToString());
        if (containerSAS == null)
        {
            var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(checklistId, cancellationToken);
            _cacheRepository.SetKeyValue(checklistId, newContainerSAS, TimeSpan.FromHours(1));
        }
        //persist the blobUri with the Punch
        punch.ImageBlobUris.Add(blobUri);

        await _punchRepository.SaveChanges(cancellationToken);

        //get the sas token from cache
        //return blobUri;
    }
}
