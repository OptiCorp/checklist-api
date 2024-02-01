using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.ItemAggregate;

public class PartChildAdded : Event
{
    public required string ChildId { get; init; }

    public required string ParentId { get; set; }
}