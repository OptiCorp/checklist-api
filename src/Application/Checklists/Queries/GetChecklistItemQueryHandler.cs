
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Queries;

public class GetChecklistItemQueryHandler : IRequestHandler<GetChecklistItemQuery, ChecklistItem>
{
    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IChecklistItemRepository _checklistItemRepository;


    public GetChecklistItemQueryHandler(IMobilizationRepository mobilizationRepository, IChecklistItemRepository checklistItemRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistItemRepository = checklistItemRepository;
    } 

    public async Task<ChecklistItem> Handle(GetChecklistItemQuery request, CancellationToken cancellationToken)
    {
        // var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId, cancellationToken)
        //     ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);
        
        var checklistItem = await _checklistItemRepository.GetChecklistItem(request.ChecklistItemId, cancellationToken) ??
             throw new NotFoundException(nameof(ChecklistItem), request.ChecklistItemId);
        //var checklist = mobilization.Checklist;
        
        //await _checklistItemRepository.LoadChecklistItems(checklist, cancellationToken);
        return checklistItem;
    } 
}
