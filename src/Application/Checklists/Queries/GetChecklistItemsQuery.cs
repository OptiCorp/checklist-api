
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Checklists.Queries;

public class GetChecklistItemsQuery : IRequest<IEnumerable<ChecklistItem>>
{
    public Guid MobilizationId {get; init;}
}
