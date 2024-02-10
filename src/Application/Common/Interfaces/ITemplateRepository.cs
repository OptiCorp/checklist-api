using MobDeMob.Domain.ItemAggregate;

namespace Application.Common.Interfaces;

public interface ITemplateRepository
{
    Task<string> AddTemplate(ItemTemplate template, CancellationToken cancellationToken = default);

    Task<ItemTemplate?> GetTemplateById(string templateId, CancellationToken cancellationToken = default);

    Task<ItemTemplate?> GetTemplateByItemId(string itemId, CancellationToken cancellationToken = default);

    Task SaveChanges(CancellationToken cancellationToken = default);
}
