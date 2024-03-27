using Application.Checklists.Dtos;
using Application.Common.Models;
using Application.Mobilizations.Dtos;
using MediatR;
using MobDeMob.Domain.Entities;

namespace Application.Checklists.Queries;

public class GetChecklistsForItemBySearchQuery : IRequest<PaginatedList<ChecklistBriefDto>>
{
    public string itemId { get; init; }

    public string checklistSearchId {get; init;}

    public int pageNumber { get; init; } = 1;

    public int pageSize { get; init; } = 10;
}
