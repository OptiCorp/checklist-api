
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Punches.Dtos;
using AutoMapper;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace Application.Checklists.Queries;

public class GetPunchesQueryHandler : IRequestHandler<GetPunchesQuery, PunchListDto>
{

    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IChecklistItemRepository _checklistItemRepository;

    private readonly ICacheRepository _cacheRepository;

    private readonly IFileStorageRepository _fileStorageRepository;

    private readonly IMapper _mapper;

    public GetPunchesQueryHandler(IMobilizationRepository mobilizationRepository, IChecklistItemRepository checklistItemRepository, ICacheRepository cacheRepository, IFileStorageRepository fileStorageRepository, IMapper mapper)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistItemRepository = checklistItemRepository;
        _cacheRepository = cacheRepository;
        _fileStorageRepository = fileStorageRepository;
        _mapper = mapper;
    }
    public async Task<PunchListDto> Handle(GetPunchesQuery request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId)
            ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId); ;

        var punches = await _checklistItemRepository.GetChecklistItemsWithPunches(mobilization.ChecklistId, cancellationToken);
        var punchesDto = punches.Select(p => _mapper.Map<PunchDto>(p));
        if (!punches.Any())
        {
            return MapToPunchDtoList([]);
        }
        else
        {
            var anyBlobUrisInPunches = punches.Any(p => p.ImageBlobUris.Any());
            if (!anyBlobUrisInPunches) return MapToPunchDtoList(punchesDto);
        }

     

        var checklistId = mobilization.ChecklistId;
        var containerSAS = _cacheRepository.GetValue(checklistId.ToString());
        if (containerSAS == null)
        {
            var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(checklistId.ToString(), cancellationToken);
            _cacheRepository.SetKeyValue(checklistId.ToString(), newContainerSAS, TimeSpan.FromHours(1));
            //punchDtos = punches.Select(p => p.AsDto());
            return MapToPunchDtoList(punchesDto, newContainerSAS.Query);
        }
        //punchDtos = punches.Select(p => p.AsDto());
        return MapToPunchDtoList(punchesDto, containerSAS.Query);
        // return punches.Select(p => p.AsDto(containerSAS));
    }

    private static PunchListDto MapToPunchDtoList(IEnumerable<PunchDto> punchDtos, string? SASToken = null)
    {
        return new PunchListDto()
        {
            SASToken = SASToken,
            Punches = punchDtos
        };
    }
}