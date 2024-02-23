
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

    private readonly IChecklistItemRepository _checklistItemRepository;

    private readonly IPunchRepository _punchRepository;

    private readonly ICacheRepository _cacheRepository;

    private readonly IFileStorageRepository _fileStorageRepository;



    public GetPunchesQueryHandler(IMobilizationRepository mobilizationRepository, IChecklistItemRepository checklistItemRepository, ICacheRepository cacheRepository, IFileStorageRepository fileStorageRepository, IPunchRepository punchRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistItemRepository = checklistItemRepository;
        _cacheRepository = cacheRepository;
        _fileStorageRepository = fileStorageRepository;
        _punchRepository = punchRepository;

    }
    public async Task<PunchListDto> Handle(GetPunchesQuery request, CancellationToken cancellationToken)
    {
        var checklistItem = await _checklistItemRepository.GetChecklistItemWithTemplate(request.ChecklistItemId, cancellationToken)
             ?? throw new NotFoundException(nameof(ChecklistItem), request.ChecklistItemId);

        var punches = await _punchRepository.GetPunchesForChecklistItem(request.ChecklistItemId, cancellationToken);
        
        var punchesIds = punches.Select(p => p.Id);
        var itemTemplateDto = checklistItem.Template.Adapt<ItemTemplateDto>();

        if (!punches.Any() || !punches.Any(p => p.ImageBlobUris.Any()))
        {
            return MapToPunchDtoList(punchesIds, itemTemplateDto, checklistItem.Id, null);
        }

        var checklistId = checklistItem.ChecklistId;
        var containerSAS = _cacheRepository.GetValue(checklistId.ToString());

        if (containerSAS == null)
        {
            var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(checklistId.ToString(), cancellationToken);
            _cacheRepository.SetKeyValue(checklistId.ToString(), newContainerSAS, TimeSpan.FromHours(1));
            return MapToPunchDtoList(punchesIds, itemTemplateDto, checklistId, newContainerSAS.Query);
        }

        return MapToPunchDtoList(punchesIds, itemTemplateDto, checklistId, containerSAS.Query);
    }

    private static PunchListDto MapToPunchDtoList(IEnumerable<Guid> punchesIds, ItemTemplateDto itemTemplate, Guid checklistItemId, string? SASToken = null)
    {
        return new PunchListDto(punchesIds, itemTemplate, checklistItemId, SASToken);
    }

}