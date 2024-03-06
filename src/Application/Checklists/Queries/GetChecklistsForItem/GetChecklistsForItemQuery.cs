using Application.Checklists.Dtos;
using Application.Common.Models;
using Application.Mobilizations.Dtos;
using MediatR;

namespace Application.Mobilizations.Queries;

public class GetChecklistsForItemQuery: IRequest<PaginatedList<ChecklistBriefDto>>
{
    public string ItemId { get; init; }

    public int PageNumber {get; init;}

    public int PageSize {get; init;}
}
