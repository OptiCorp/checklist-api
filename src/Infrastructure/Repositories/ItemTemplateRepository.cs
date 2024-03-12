using Application.Common.Interfaces;
using Domain.Entities.TemplateAggregate;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.ItemAggregate;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories;

public class ItemTemplateRepository : RepositoryBase<ItemTemplate>, IItemTemplateRepository
{
    public ItemTemplateRepository(ModelContextBase modelContextBase) : base(modelContextBase) 
    {
    }
  

    public async Task<Guid> AddTemplate(ItemTemplate itemTemplate, CancellationToken cancellationToken = default)
    {
        await Add(itemTemplate, cancellationToken);
        return itemTemplate.Id;
    }

    public async Task DeleteItemTemplate(Guid Id, CancellationToken cancellationToken = default)
    => await DeleteById(Id, cancellationToken);

    public async Task<ItemTemplate?> GetTemplateById(Guid templateId, CancellationToken cancellationToken = default)
    {
        return await GetSet()
            .Include(t => t.Questions) //TODO: should getting questions be a separate repository call?
            .FirstOrDefaultAsync(x => x.Id == templateId, cancellationToken);
    }

    public async Task<ItemTemplate?> GetTemplateByItemId(string itemId, CancellationToken cancellationToken = default)
    {
        return await _modelContextBase.ItemTemplates
            .Include(t => t.Questions)
            .SingleOrDefaultAsync(x => x.ItemId == itemId, cancellationToken);
    }

    public async Task<Dictionary<string, bool>> ItemTemplateExistsForItemIds(IEnumerable<string> itemIds, CancellationToken cancellationToken = default)
    {
        var itemIdsHash = new HashSet<string>(itemIds);
        var itemHasItemTemplate = new Dictionary<string, bool>();

        var tasks = await GetSet()
            .Where(it => itemIdsHash.Contains(it.ItemId))
            .Select(it => new { it.ItemId, Exists = true })
            .ToListAsync(cancellationToken: cancellationToken);

        foreach (var item in tasks)
        {
            itemHasItemTemplate[item.ItemId] = item.Exists;
        }

        foreach (var itemId in itemIds)
        {
            if (!itemHasItemTemplate.ContainsKey(itemId))
            {
                itemHasItemTemplate[itemId] = false;
            }
        }

        return itemHasItemTemplate;
    }
}
