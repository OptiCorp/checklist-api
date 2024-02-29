
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

public class GetChecklistsQueryHandler : IRequestHandler<GetChecklistsQuery, PaginatedList<ChecklistBriefDto>> 
{
    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IChecklistRepository _checklistRepository;


    public GetChecklistsQueryHandler(IMobilizationRepository mobilizationRepository, IChecklistRepository checklistRepository)
    {
        _mobilizationRepository = mobilizationRepository;
        _checklistRepository = checklistRepository; 
    }
    public async Task<PaginatedList<ChecklistBriefDto>> Handle(GetChecklistsQuery request, CancellationToken cancellationToken)
    {
        var mobilization = await _mobilizationRepository.GetMobilizationById(request.MobilizationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Mobilization), request.MobilizationId);

        var checklistItems = await _checklistRepository.GetChecklistsWithPaginationFromChecklistCollection(mobilization.ChecklistCollectionId, request.PageNumber, request.PageSize, cancellationToken);
        //var checklistItemsDto = checklistItems.AsQueryable().ProjectToType<ChecklistItemDto>();

        //var checklist = mobilization.Checklist;

        //await _checklistItemRepository.LoadChecklistItems(checklist, cancellxationToken);
        return checklistItems;
    }

}
