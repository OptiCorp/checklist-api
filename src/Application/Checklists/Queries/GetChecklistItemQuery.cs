
using Application.Checklists.Dtos;
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Queries;

public class GetChecklistItemQuery : IRequest<ChecklistItemDto>
{
    public Guid ChecklistItemId {get; init;}
}
