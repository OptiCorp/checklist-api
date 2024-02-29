using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories;

public class ItemRepository : IItemReposiory
{
    private readonly ModelContextBase _modelContextBase;




    public ItemRepository(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;

    }
    public async Task<Item?> GetItemById(string Id, CancellationToken cancellationToken = default)
    {
        return await _modelContextBase.Items
            .Where(i => i.Id == Id)
            .SingleOrDefaultAsync(cancellationToken);
    }
}