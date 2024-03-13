
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IItemReposiory
{
    Task<Item?> GetItemById (string Id, CancellationToken cancellationToken = default);

    Task AddItem (string Id, CancellationToken cancellationToken = default);

}