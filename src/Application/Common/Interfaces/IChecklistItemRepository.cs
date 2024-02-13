using Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace Application.Common.Interfaces
{
    public interface IChecklistItemRepository
    {
        Task<Guid> AddChecklistItem(ChecklistItem checklistItem, CancellationToken cancellationToken = default);

        Task<ChecklistItem?> GetChecklistItemByItemId (string itemId, Guid checklistId, CancellationToken cancellationToken = default);

        Task<IEnumerable<Punch>> GetChecklistItemsWithPunches(Guid checklistId, CancellationToken cancellationToken = default);
        Task SaveChanges(CancellationToken cancellationToken);
    }
}
