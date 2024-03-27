
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IItemReposiory
{
    Task<Item?> GetItemByIdNoTracking(string Id, CancellationToken cancellationToken = default);

    Task<Item?> GetItemById(string Id, CancellationToken cancellationToken = default);

    Task AddItem(Item item, CancellationToken cancellationToken = default);

    Task DeleteItemById(string id, CancellationToken cancellationToken = default);

    Task<Dictionary<string, bool>> ChecklistTemplateExistsForItemIds(IEnumerable<string> itemIds, CancellationToken cancellationToken = default);

}