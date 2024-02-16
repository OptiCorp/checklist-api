
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Queries;

public class GetChecklistItemsQueryHandler : IRequestHandler<GetChecklistItemsQuery, IEnumerable<ChecklistItem>>
{
    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IChecklistItemRepository _checklistItemRepository;


    public GetChecklistItemsQueryHandler(IMobilizationRepository mobilizationRepository, IChecklistItemRepository checklistItemRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistItemRepository = checklistItemRepository;
    }
    public async Task<IEnumerable<ChecklistItem>> Handle(GetChecklistItemsQuery request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);
        
        var checklistItems = await _checklistItemRepository.GetChecklistItems(mobilization.ChecklistId, cancellationToken);
        //var checklist = mobilization.Checklist;
        
        //await _checklistItemRepository.LoadChecklistItems(checklist, cancellationToken);
        return checklistItems;
    }   
}
