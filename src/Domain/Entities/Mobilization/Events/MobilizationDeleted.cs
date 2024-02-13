using MobDeMob.Domain.Common;

namespace Domain.Entities.Mobilization.Events
{
    public record MobilizationDeleted(Guid MobilizationId) : IDomainEvent;
}
