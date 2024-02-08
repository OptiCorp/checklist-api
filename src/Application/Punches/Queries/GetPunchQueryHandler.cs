


using Application.Common.Interfaces;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Application.Parts;
using MobDeMob.Application.Punches;
using MobDeMob.Application.Punches.Queries;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Punches.Queries;

public class GetPunchQueryHandler : IRequestHandler<GetPunchQuery, PunchDto?>

{
    private readonly IPunchRepository _punchRepository;

    private readonly IFileStorageRepository _fileStorageRepository;

    private readonly ICacheRepository _cacheRepository;

    public GetPunchQueryHandler(IPunchRepository punchRepository, IFileStorageRepository fileStorageRepository, ICacheRepository cachRepository)
    {
        _punchRepository = punchRepository;
        _fileStorageRepository = fileStorageRepository;
        _cacheRepository = cachRepository;
    }

    public async Task<PunchDto?> Handle(GetPunchQuery request, CancellationToken cancellationToken)
    {
        var punch = await _punchRepository.GetPunch(request.punchId, cancellationToken);
        if (punch != null)
        {
            var blobUri = punch.ImageBlobUri;
            if (blobUri != null && punch.ParantChecklistSectionId != null)
            {
                var containerSasUri = _cacheRepository.GetValue(punch.SectionId);
                if (containerSasUri == null)
                {
                    var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(punch.ParantChecklistSectionId, cancellationToken);
                    _cacheRepository.SetKeyValue(punch.ParantChecklistSectionId, newContainerSAS);
                    var blobUriWithSas = _fileStorageRepository.ConcatBlobUriWithContainerSasTokenUri(blobUri, newContainerSAS);

                    return punch.AsDto(blobUriWithSas);
                }
                else
                {
                    var blobUriWithSas = _fileStorageRepository.ConcatBlobUriWithContainerSasTokenUri(blobUri, containerSasUri);
                    return punch.AsDto(blobUriWithSas);
                }
            }
            return punch.AsDto(null);
        }
        return null;
    }
}