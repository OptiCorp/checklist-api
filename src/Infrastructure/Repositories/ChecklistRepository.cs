using Infrastructure.Repositories.Common;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities.ChecklistAggregate;


namespace MobDeMob.Infrastructure.Repositories;

public class CheklistRepository : RepositoryBase<Checklist>, IChecklistRepository
{
    public CheklistRepository(ModelContextBase modelContextBase) : base(modelContextBase)
    {
    }

    public async Task<Guid> AddChecklist(Checklist checklist, CancellationToken cancellationToken = default)
    {
        await Add(checklist, cancellationToken);
        return checklist.Id;
    }


    public async Task DeleteChecklist(Guid id, CancellationToken cancellationToken)
    => await DeleteById(id, cancellationToken);

    public void RemovePartFromChecklist(Checklist checklist, string partId)
    {
        checklist.Parts.Remove(partId);
    }
}
