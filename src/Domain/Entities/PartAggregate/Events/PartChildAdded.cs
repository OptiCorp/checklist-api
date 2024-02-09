using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.ItemAggregate;

public class PartChildAdded : IDomainEvent
{
    public required string ChildId { get; init; }

    public required string ParentId { get; set; }
}
