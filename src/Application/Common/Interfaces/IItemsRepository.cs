using MobDeMob.Domain.Entities.ItemAggregate;

namespace MobDeMob.Application.Common.Interfaces;
public interface IItemsRepository
{
    Task AddItem(Item item, CancellationToken cancellationToken = default);
    Task<Item?> GetById(string id);
}
