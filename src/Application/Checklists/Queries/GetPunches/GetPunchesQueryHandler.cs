
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Punches;
using Application.Punches.Dtos;
using Application.Templates;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MediatR;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Queries;

public class GetPunchesQueryHandler : IRequestHandler<GetPunchesQuery, PunchListDto>
{

    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IChecklistRepository _checklistRepository;

    private readonly IPunchRepository _punchRepository;

    private readonly ICacheRepository _cacheRepository;

    private readonly IFileStorageRepository _fileStorageRepository;



    public GetPunchesQueryHandler(IMobilizationRepository mobilizationRepository, IChecklistRepository checklistRepository, ICacheRepository cacheRepository, IFileStorageRepository fileStorageRepository, IPunchRepository punchRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistRepository = checklistRepository;
        _cacheRepository = cacheRepository;
        _fileStorageRepository = fileStorageRepository;
        _punchRepository = punchRepository;

    }
    public async Task<PunchListDto> Handle(GetPunchesQuery request, CancellationToken cancellationToken)
    {
        var checklist = await _checklistRepository.GetChecklistWithTemplate(request.ChecklistId, cancellationToken)
             ?? throw new NotFoundException(nameof(Checklist), request.ChecklistId);

        var punches = await _punchRepository.GetPunchesForChecklist(request.ChecklistId, cancellationToken);
        
        var punchesIds = punches.Select(p => p.Id);

        var itemTemplateDto = checklist.ItemTemplate.Adapt<ItemTemplateDto>();

        if (!punches.Any() || !punches.Any(p => p.PunchFiles.Any()))
        {
            return MapToPunchDtoList(punchesIds, itemTemplateDto, checklist.Id, null);
        }

        var checklistCollectionId = checklist.ChecklistCollectionId;
        var containerSAS = _cacheRepository.GetValue(checklistCollectionId.ToString());

        if (containerSAS == null)
        {
            var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(checklistCollectionId.ToString(), cancellationToken);
            _cacheRepository.SetKeyValue(checklistCollectionId.ToString(), newContainerSAS, TimeSpan.FromHours(1));
            return MapToPunchDtoList(punchesIds, itemTemplateDto, checklistCollectionId, newContainerSAS.Query);
        }

        return MapToPunchDtoList(punchesIds, itemTemplateDto, checklistCollectionId, containerSAS.Query);
    }

    private static PunchListDto MapToPunchDtoList(IEnumerable<Guid> punchesIds, ItemTemplateDto itemTemplate, Guid checklistItemId, string? SASToken = null)
    {
        return new PunchListDto(punchesIds, itemTemplate, checklistItemId, SASToken);
    }

}