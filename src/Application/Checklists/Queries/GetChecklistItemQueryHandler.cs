
using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Punches;
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MediatR;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Queries;

public class GetChecklistItemQueryHandler : IRequestHandler<GetChecklistItemQuery, ChecklistItemDto>
{
    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IChecklistItemRepository _checklistItemRepository;

    private readonly IPunchRepository _punchRepository;



    public GetChecklistItemQueryHandler(IMobilizationRepository mobilizationRepository, IChecklistItemRepository checklistItemRepository, IPunchRepository punchRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistItemRepository = checklistItemRepository;
        _punchRepository = punchRepository;
    } 

    public async Task<ChecklistItemDto> Handle(GetChecklistItemQuery request, CancellationToken cancellationToken)
    {
        // var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId, cancellationToken)
        //     ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);
        
        var checklistItem = await _checklistItemRepository.GetChecklistItem(request.ChecklistItemId, cancellationToken) ??
             throw new NotFoundException(nameof(ChecklistItem), request.ChecklistItemId);

        checklistItem.PunchesCount = await _punchRepository.GetPunchesCount(checklistItem.Id, cancellationToken);
        
        var checklistItemDto = checklistItem.Adapt<ChecklistItemDto>();
        //var checklist = mobilization.Checklist;
        
        //await _checklistItemRepository.LoadChecklistItems(checklist, cancellationToken);
        return checklistItemDto;
    } 
}
