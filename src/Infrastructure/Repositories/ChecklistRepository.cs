using Infrastructure.Repositories.Common;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities.ChecklistAggregate;


namespace MobDeMob.Infrastructure.Repositories;

public class CheklistRepository : RepositoryBase<Checklist>, IChecklistRepository
{
    public CheklistRepository(ModelContextBase modelContextBase) : base(modelContextBase)
    {
    }

    public async Task<string> AddChecklist(Checklist checklist, CancellationToken cancellationToken = default)
    {
        await Add(checklist, cancellationToken);
        return checklist.Id;
    }
}
