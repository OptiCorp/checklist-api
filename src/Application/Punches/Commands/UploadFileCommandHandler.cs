using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Punches;

namespace MobDeMob.Application.Punches.Commands;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Uri>
{
    private readonly IFileStorageRepository _fileStorageRepository;
    private readonly ICacheRepository _cacheRepository;
    private readonly IPunchRepository _punchRepository;


    public UploadFileCommandHandler(IFileStorageRepository fileStorageRepository, ICacheRepository cachRepository, IPunchRepository punchRepository)
    {
        _fileStorageRepository = fileStorageRepository;
        _cacheRepository = cachRepository;
        _punchRepository = punchRepository;
    }

    public async Task<Uri> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var punch = await _punchRepository.GetPunch(request.PunchId, cancellationToken) ?? throw new Exception($"Punch with id: '{request.PunchId}' does not exist");

        var blobUri = await _fileStorageRepository.UploadImage(request.Stream, request.FileName, punch.ParantChecklistSectionId, request.ContentType, cancellationToken);
        var containerSAS = _cacheRepository.GetValue(punch.ParantChecklistSectionId);
        if (containerSAS == null)
        {
            var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(punch.ParantChecklistSectionId, cancellationToken);
            _cacheRepository.SetKeyValue(punch.ParantChecklistSectionId, newContainerSAS, TimeSpan.FromHours(1));
        }
        //persist the blobUri with the Punch
        await _punchRepository.AssociatePunchWithUrl(request.PunchId, blobUri, cancellationToken);
        //get the sas token from cache
        return blobUri;
    }
}
