using Domain.Entities.ChecklistAggregate;

namespace Application.Common.Interfaces
{
    public interface IChecklistItemRepository
    {
        Task<string> AddChecklistItem(ChecklistItem checklistItem, CancellationToken cancellationToken = default);
        Task SaveChanges(CancellationToken cancellationToken);
    }
}
