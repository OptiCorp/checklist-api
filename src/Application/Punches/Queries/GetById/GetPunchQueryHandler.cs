using Application.Punches.Dtos;
using Mapster;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;

namespace Application.Punches.Queries.GetById;

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
        PunchDto punchDto;
        if (punch != null)
        {
            var blobUris = punch.ImageBlobUris;
            if (blobUris.Count == 0) return punch.Adapt<PunchDto>();;

            var checklistId = punch.ChecklistItem.ChecklistId;
            var containerSasUri = _cacheRepository.GetValue(checklistId.ToString());
            if (containerSasUri == null)
            {
                var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(checklistId.ToString(), cancellationToken);
                _cacheRepository.SetKeyValue(checklistId.ToString(), newContainerSAS);
                //var blobUriWithSas = _fileStorageRepository.ConcatBlobUriWithContainerSasTokenUri(blobUri, newContainerSAS);
                punch.SasToken = newContainerSAS.Query.ToString();
                punchDto = punch.Adapt<PunchDto>();
                return punchDto;
            }
            else
            {
                //var blobUriWithSas = _fileStorageRepository.ConcatBlobUriWithContainerSasTokenUri(blobUri, containerSasUri);
                punch.SasToken = containerSasUri.Query.ToString();
                punchDto = punch.Adapt<PunchDto>();
                return punchDto;
            }

        }
        return null;
    }
}
