using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.ItemAggregate;

internal class PartChildRemoved : Event
{
    public required string ChildId { get; init; }

    public required string ParentId { get; set; }
}