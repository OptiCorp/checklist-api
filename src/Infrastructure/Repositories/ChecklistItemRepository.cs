using Application.Checklists.Dtos;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using Infrastructure.Repositories.Common;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Infrastructure;
using Application.Common.Mappings;


namespace Infrastructure.Repositories
{
    public class ChecklistItemRepository : RepositoryBase<ChecklistItem>, IChecklistItemRepository
    {
        public ChecklistItemRepository(ModelContextBase modelContextBase) : base(modelContextBase)
        {

        }

        public async Task<Guid> AddChecklistItem(ChecklistItem checklistItem, CancellationToken cancellationToken = default)
        {
            await Add(checklistItem, cancellationToken);

            return checklistItem.Id;
        }

        public async Task<ChecklistItem?> GetChecklistItemByItemId(string itemId, Guid checklistId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Where(ci => ci.ChecklistId == checklistId)
                .SingleOrDefaultAsync(ci => ci.ItemId == itemId, cancellationToken);
        }

        public async Task<IEnumerable<ChecklistItem>> GetChecklistItems(Guid checklistId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Include(ci => ci.Template)
                .Where(ci => ci.ChecklistId == checklistId)
                .ToListAsync(cancellationToken);
        }

        public async Task<ChecklistItem?> GetChecklistItem(Guid checklistItemId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Include(c => c.Questions)
                //.Include(c => c.Template)
                .SingleOrDefaultAsync(c => c.Id == checklistItemId, cancellationToken);
        }

        public async Task DeleteChecklistItem(Guid Id, CancellationToken cancellationToken = default)
        {
            await DeleteById(Id, cancellationToken);
        }

        public void RemoveChecklistItem(ChecklistItem checklistItem, CancellationToken cancellationToken = default)
        {
            Remove(checklistItem);
        }

        public async Task<ChecklistItem?> GetChecklistItemWithTemplate(Guid checklistItemId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                 .Include(c => c.Template)
                 //.Include(c => c.Template)
                 .SingleOrDefaultAsync(c => c.Id == checklistItemId, cancellationToken);
        }

        public async Task<PaginatedList<ChecklistItemBriefDto>> GetChecklistItemsWithPagination(Guid checklistId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Where(ci => ci.ChecklistId == checklistId)
                .OrderBy(x => x.Created)
                .ProjectToType<ChecklistItemBriefDto>()
                .PaginatedListAsync(pageNumber, pageSize);
        }
    }
}
