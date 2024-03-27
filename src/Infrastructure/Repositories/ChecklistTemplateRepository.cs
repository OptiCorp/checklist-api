using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories.Common;

public class ChecklistTemplateRespository : RepositoryBase<ChecklistTemplate>, IChecklistTemplateRepository
{
    public ChecklistTemplateRespository(ModelContextBase modelContextBase) : base(modelContextBase)
    {
    }

    public async Task<ChecklistTemplate> CreateChecklistTemplate(string itemTemplateId, CancellationToken cancellationToken)
    {
        var checklistTemplate = ChecklistTemplate.New(itemTemplateId);  
        await Add(checklistTemplate, cancellationToken);
        return checklistTemplate;
    }

    public async Task<ChecklistTemplate?> GetChecklistTemplateById(Guid id, CancellationToken cancellationToken = default)
        => await GetSet()
            .Include(ct => ct.Questions)
            .SingleOrDefaultAsync(ct => ct.Id == id, cancellationToken);

    public async Task<ChecklistTemplate?> GetChecklistTemplateByItemTemplateId(string itemTemplateId, CancellationToken cancellationToken = default)
    {
        return await GetSet()
            .AsNoTracking()
            .Include(ct => ct.Questions)
            .SingleOrDefaultAsync(ct => ct.ItemTemplateId == itemTemplateId, cancellationToken);
    }
}