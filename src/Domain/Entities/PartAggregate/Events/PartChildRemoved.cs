using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.ItemAggregate;

internal class PartChildRemoved : IDomainEvent
{
    public required string ChildId { get; init; }

    public required string ParentId { get; set; }
}
