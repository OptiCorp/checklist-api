using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories.Common;
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

    public async Task AddItem(Item item, CancellationToken cancellationToken = default)
    {
        await _modelContextBase.Items.AddAsync(item, cancellationToken);
        await _modelContextBase.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteItemById(string id, CancellationToken cancellationToken = default)
    {
        await _modelContextBase.Items.Where(i => i.Id == id).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<Item?> GetItemById(string Id, CancellationToken cancellationToken = default)
    {
        return await _modelContextBase.Items
            .Where(i => i.Id == Id)
            .SingleOrDefaultAsync(cancellationToken);
    }
}