using Infrastructure.Repositories.Common;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Domain.Entities.ChecklistAggregate;


namespace MobDeMob.Infrastructure.Repositories;

public class CheklistCollectionRepository : RepositoryBase<ChecklistCollection>, IChecklistCollectionRepository
{
    public CheklistCollectionRepository(ModelContextBase modelContextBase) : base(modelContextBase)
    {
    }

    public async Task<Guid> AddChecklist(ChecklistCollection checklistCollection, CancellationToken cancellationToken = default)
    {
        await Add(checklistCollection, cancellationToken);
        return checklistCollection.Id;
    }


    public async Task DeleteChecklistCollection(Guid id, CancellationToken cancellationToken)
    => await DeleteById(id, cancellationToken);

    // public void RemovePartFromChecklist(ChecklistCollection checklist, string partId)
    // {
    //     checklist.Parts.Remove(partId);
    // }
}
