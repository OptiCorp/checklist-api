using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using Infrastructure.Repositories.Common;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories
{
    public class ChecklistItemRepository : RepositoryBase<ChecklistItem>, IChecklistItemRepository
    {
        public ChecklistItemRepository(ModelContextBase modelContextBase) : base(modelContextBase) { }

        public async Task<string> AddChecklistItem(ChecklistItem checklistItem, CancellationToken cancellationToken = default)
        {
            await Add(checklistItem, cancellationToken);

            return checklistItem.Id;
        }
    }
}
