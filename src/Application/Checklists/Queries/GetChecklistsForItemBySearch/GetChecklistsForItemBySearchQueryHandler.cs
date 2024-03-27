


using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Mobilizations.Dtos;
using Mapster;
using MediatR;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Domain.Entities;

namespace Application.Checklists.Queries;

public class GetChecklistsForItemBySearchQueryHandler : IRequestHandler<GetChecklistsForItemBySearchQuery, PaginatedList<ChecklistBriefDto>>

{
    private readonly IChecklistRepository _checklistRepository;

    private readonly IMobilizationRepository _mobilizationRepository;
    public GetChecklistsForItemBySearchQueryHandler(IChecklistRepository checklistRepository, IMobilizationRepository mobilizationRepository)
    {
        _checklistRepository = checklistRepository;
        _mobilizationRepository = mobilizationRepository;
    }

    public async Task<PaginatedList<ChecklistBriefDto>> Handle(GetChecklistsForItemBySearchQuery request, CancellationToken cancellationToken)
    {
        var checklistsSearchedPaginated = await _checklistRepository
            .GetChecklistsForItemBySearch(request.itemId, request.checklistSearchId, request.pageNumber, request.pageSize, cancellationToken);

        foreach (var checklist in checklistsSearchedPaginated.Items)
        {
            var belongingMob = await _mobilizationRepository.GetMobilizationIdByChecklistCollectionId(checklist.ChecklistCollectionId, cancellationToken)
                ?? throw new NotFoundException(nameof(Mobilization), $"Could not find mobilization based on checklistcollectionId: '{checklist.ChecklistCollectionId}'");
            checklist.SetMobilizationId(belongingMob.Id);
        };

        var checklistsPaginatedDtos = new PaginatedList<ChecklistBriefDto>(
                checklistsSearchedPaginated.Items.AsQueryable().ProjectToType<ChecklistBriefDto>(),
                checklistsSearchedPaginated.TotalCount,
                checklistsSearchedPaginated.PageNumber,
                checklistsSearchedPaginated.TotalPages
        );

        return checklistsPaginatedDtos;
    }


}
