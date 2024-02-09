using MobDeMob.Domain.Common;

namespace Domain.Entities.Mobilization.Events
{
    public record MobilizationDeleted(string MobilizationId) : IDomainEvent;
}
