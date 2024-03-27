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
            .AsNoTracking()
            .Include(i => i.ItemTemplate)
            .Where(i => i.Id == Id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Dictionary<string, bool>> ChecklistTemplateExistsForItemIds(IEnumerable<string> itemIds, CancellationToken cancellationToken = default)
    {
        var itemHasItemTemplate = new Dictionary<string, bool>();

        var tasks = await _modelContextBase.Items
            .AsNoTracking()
            .Include(i => i.ItemTemplate)
            .ThenInclude(it => it.ChecklistTemplate)
            .Where(i => itemIds.Contains(i.Id))
            .Select(i => new { i.Id, Exists = i.ItemTemplate.ChecklistTemplate != null })
            .ToListAsync(cancellationToken: cancellationToken);

        foreach (var item in tasks)
        {
            itemHasItemTemplate[item.Id] = item.Exists;
        }

        // foreach (var itemId in itemIds)
        // {
        //     if (!itemHasItemTemplate.ContainsKey(itemId))
        //     {
        //         itemHasItemTemplate[itemId] = false;
        //     }
        // }

        return itemHasItemTemplate;
    }
}