using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Common.Interfaces;
public interface IItemsRepository
{
    Task AddItem(Part part, CancellationToken cancellationToken);
    Task<Part?> GetById(string id, CancellationToken cancellationToken);
}
