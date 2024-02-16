using MobDeMob.Domain.ItemAggregate;

namespace Application.Common.Interfaces;

public interface ITemplateRepository
{
    Task<Guid> AddTemplate(ItemTemplate template, CancellationToken cancellationToken = default);

    Task<ItemTemplate?> GetTemplateById(Guid templateId, CancellationToken cancellationToken = default);

    Task<ItemTemplate?> GetTemplateByItemId(string itemId, CancellationToken cancellationToken = default);

    Task DeletePartTemplate(Guid Id, CancellationToken cancellationToken = default);

    Task SaveChanges(CancellationToken cancellationToken = default);
}
