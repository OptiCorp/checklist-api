using Application.Punches.Dtos;
using AutoMapper;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;

namespace Application.Punches.Queries.GetById;

public class GetPunchQueryHandler : IRequestHandler<GetPunchQuery, PunchDto?>

{
    private readonly IPunchRepository _punchRepository;

    private readonly IFileStorageRepository _fileStorageRepository;

    private readonly ICacheRepository _cacheRepository;

    private readonly IMapper _mapper;
    public GetPunchQueryHandler(IPunchRepository punchRepository, IFileStorageRepository fileStorageRepository, ICacheRepository cachRepository, IMapper mapper)
    {
        _punchRepository = punchRepository;
        _fileStorageRepository = fileStorageRepository;
        _cacheRepository = cachRepository;
        _mapper = mapper;
    }

    public async Task<PunchDto?> Handle(GetPunchQuery request, CancellationToken cancellationToken)
    {
        var punch = await _punchRepository.GetPunch(request.punchId, cancellationToken);
        var punchDto = _mapper.Map<PunchDto>(punch);
        if (punch != null)
        {
            var blobUris = punch.ImageBlobUris;
            if (blobUris.Count == 0) return punchDto;

            var checklistId = punch.ChecklistItem.ChecklistId;
            var containerSasUri = _cacheRepository.GetValue(checklistId.ToString());
            if (containerSasUri == null)
            {
                var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(checklistId.ToString(), cancellationToken);
                _cacheRepository.SetKeyValue(checklistId.ToString(), newContainerSAS);
                //var blobUriWithSas = _fileStorageRepository.ConcatBlobUriWithContainerSasTokenUri(blobUri, newContainerSAS);
                punchDto.SASToken = newContainerSAS.Query.ToString();
                return punchDto;
            }
            else
            {
                //var blobUriWithSas = _fileStorageRepository.ConcatBlobUriWithContainerSasTokenUri(blobUri, containerSasUri);
                punchDto.SASToken = containerSasUri.Query.ToString();
                return punchDto;
            }

        }
        return null;
    }
}
