using Application.Common.Interfaces;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.ItemAggregate;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories;

public class TemplateRepository : RepositoryBase<ItemTemplate>, ITemplateRepository
{
    public TemplateRepository(ModelContextBase modelContextBase) : base(modelContextBase)
    {
    }

    public async Task<Guid> AddTemplate(ItemTemplate itemTemplate, CancellationToken cancellationToken = default)
    {
        await Add(itemTemplate, cancellationToken);
        return itemTemplate.Id;
    }

    public async Task<ItemTemplate?> GetTemplateById(Guid templateId, CancellationToken cancellationToken = default)
    {
        return await GetSet()
            .Include(t => t.Questions)
            .FirstOrDefaultAsync(x => x.Id == templateId, cancellationToken);
    }

    public async Task<ItemTemplate?> GetTemplateByItemId(string itemId, CancellationToken cancellationToken = default)
    {
        return await _modelContextBase.ItemTemplates
            .Include(t => t.Questions)
            .FirstOrDefaultAsync(x => x.ItemId == itemId, cancellationToken);
    }
}
