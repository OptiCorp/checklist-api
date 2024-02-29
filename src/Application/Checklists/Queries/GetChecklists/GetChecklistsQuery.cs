
using Application.Checklists.Dtos;
using Application.Common.Models;
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Queries;

public class GetChecklistsQuery : IRequest<PaginatedList<ChecklistBriefDto>> 
{
    public Guid MobilizationId {get; init;}

    public int PageNumber {get; init;} = 1;

    public int PageSize {get; init;} = 10;
}


