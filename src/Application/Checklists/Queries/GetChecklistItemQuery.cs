
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Queries;

public class GetChecklistItemQuery : IRequest<ChecklistItem>
{
    public Guid ChecklistItemId {get; init;}
}
