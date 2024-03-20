
using Application.Checklists.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Mobilizations.Dtos;
using Domain.Entities;
using Mapster;
using MediatR;
using MobDeMob.Domain.Entities;

namespace Application.Mobilizations.Queries;

public class GetMobilizationForItemQueryHandler : IRequestHandler<GetChecklistsForItemQuery, PaginatedList<ChecklistBriefDto>>
{
    private readonly IChecklistRepository _checklistRepository; 

    private readonly IMobilizationRepository _mobilizationRepository;

    private readonly IItemReposiory _itemReposiory;

    public GetMobilizationForItemQueryHandler(IChecklistRepository checklistRepository, IMobilizationRepository mobilizationRepository, IItemReposiory itemReposiory)
    {
        _checklistRepository = checklistRepository;
        _mobilizationRepository = mobilizationRepository;
        _itemReposiory = itemReposiory;
    }

    public async Task<PaginatedList<ChecklistBriefDto>> Handle(GetChecklistsForItemQuery request, CancellationToken cancellationToken)
    {
        var item = await _itemReposiory.GetItemById(request.ItemId) 
            ?? throw new NotFoundException(nameof(Item), request.ItemId);
        var checklistPaginated = await _checklistRepository.GetChecklistsForItem(item.Id, request.PageNumber, request.PageSize, cancellationToken);
        //var checklistCollectionIds = checklistPaginated.Items.Select(c => c.ChecklistCollectionId);

        //_ = checklistPaginated.Items.Select(async c => c.SetMobilizationId(await _mobilizationRepository.GetMobilizationIdByChecklistCollectionId(c.ChecklistCollectionId)));

        foreach (var c in checklistPaginated.Items)
        {
            var mobId = await _mobilizationRepository.GetMobilizationIdByChecklistCollectionId(c.ChecklistCollectionId, cancellationToken)
                ?? throw new NotFoundException(nameof(Mobilization), c.ChecklistCollectionId);

            c.SetMobilizationId(mobId);
            c.SetPunchesCount(c.Punches.Count);
        }

        if (!checklistPaginated.Items.Any())
        {
            
        }

        var checklistDtosPaginated = new PaginatedList<ChecklistBriefDto>(
            checklistPaginated.Items.AsQueryable().ProjectToType<ChecklistBriefDto>(),
            checklistPaginated.TotalCount,
            checklistPaginated.PageNumber,
            checklistPaginated.TotalPages
            );
        return checklistDtosPaginated;
    }
}