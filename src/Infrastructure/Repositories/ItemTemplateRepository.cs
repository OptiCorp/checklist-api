using Application.Common.Interfaces;
using Domain.Entities.TemplateAggregate;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.ItemAggregate;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories;

public class ItemTemplateRepository : IItemTemplateRepository
{
    private readonly ModelContextBase _modelContextBase;
    public ItemTemplateRepository(ModelContextBase modelContextBase)
    {
        _modelContextBase = modelContextBase;
    }


    public async Task<ItemTemplate> AddTemplate(ItemTemplate itemTemplate, CancellationToken cancellationToken = default)
    {
        await _modelContextBase.ItemTemplates.AddAsync(itemTemplate, cancellationToken);
        await _modelContextBase.SaveChangesAsync(cancellationToken);
        return itemTemplate;
    }

    public async Task DeleteItemTemplate(string id, CancellationToken cancellationToken = default)
    => await _modelContextBase.ItemTemplates.Where(m => m.Id == id).ExecuteDeleteAsync(cancellationToken);

    public async Task<ItemTemplate?> GetTemplateById(string templateId, CancellationToken cancellationToken = default)
    {
        return await _modelContextBase.ItemTemplates
            //.Include(itt => itt.Items)
            .Include(t => t.ChecklistTemplate)
            .ThenInclude(ct => ct != null ? ct.Questions : null)
            .FirstOrDefaultAsync(x => x.Id == templateId, cancellationToken);
    }


    // public async Task<ItemTemplate?> GetTemplateByItemId(string itemId, CancellationToken cancellationToken = default)
    // {
    //     return await _modelContextBase.ItemTemplates
    //         .Include(t => t.Questions)
    //         .SingleOrDefaultAsync(x => x.ItemId == itemId, cancellationToken);
    // }

    // public async Task<Dictionary<string, bool>> ItemTemplateExistsForItemIds(IEnumerable<string> itemIds, CancellationToken cancellationToken = default)
    // {
    //     var itemIdsHash = new HashSet<string>(itemIds);
    //     var itemHasItemTemplate = new Dictionary<string, bool>();

    //     var tasks = await GetSet()
    //         .Where(it => itemIdsHash.Contains(it.ItemId))
    //         .Select(it => new { it.ItemId, Exists = true })
    //         .ToListAsync(cancellationToken: cancellationToken);

    //     foreach (var item in tasks)
    //     {
    //         itemHasItemTemplate[item.ItemId] = item.Exists;
    //     }

    //     foreach (var itemId in itemIds)
    //     {
    //         if (!itemHasItemTemplate.ContainsKey(itemId))
    //         {
    //             itemHasItemTemplate[itemId] = false;
    //         }
    //     }

    //     return itemHasItemTemplate;
    // }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
        => await _modelContextBase.SaveChangesAsync(cancellationToken);

}
