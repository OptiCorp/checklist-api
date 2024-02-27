
using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MediatR;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Queries;

public class GetChecklistItemsQueryHandler : IRequestHandler<GetChecklistItemsQuery, PaginatedList<ChecklistItemBriefDto>> 
{
    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IChecklistItemRepository _checklistItemRepository;


    public GetChecklistItemsQueryHandler(IMobilizationRepository mobilizationRepository, IChecklistItemRepository checklistItemRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistItemRepository = checklistItemRepository; 
    }
    public async Task<PaginatedList<ChecklistItemBriefDto>> Handle(GetChecklistItemsQuery request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);

        var checklistItems = await _checklistItemRepository.GetChecklistItemsWithPagination(mobilization.ChecklistId, request.PageNumber, request.PageSize, cancellationToken);
        //var checklistItemsDto = checklistItems.AsQueryable().ProjectToType<ChecklistItemDto>();

        //var checklist = mobilization.Checklist;

        //await _checklistItemRepository.LoadChecklistItems(checklist, cancellxationToken);
        return checklistItems;
    }

}
