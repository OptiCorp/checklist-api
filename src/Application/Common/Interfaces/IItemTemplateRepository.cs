using MobDeMob.Domain.ItemAggregate;

namespace Application.Common.Interfaces;

public interface IItemTemplateRepository
{
    Task<string> AddTemplate(ItemTemplate template, CancellationToken cancellationToken = default);


    Task<ItemTemplate?> GetTemplateById(string templateId, CancellationToken cancellationToken = default);

    // Task<ItemTemplate?> GetTemplateByItemId(string itemId, CancellationToken cancellationToken = default);

    Task DeleteItemTemplate(string Id, CancellationToken cancellationToken = default);

    // Task<Dictionary<string, bool>> ItemTemplateExistsForItemIds (IEnumerable<string> itemIds, CancellationToken cancellationToken = default);

    Task SaveChanges(CancellationToken cancellationToken = default);
}
