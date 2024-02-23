using Application.Common.Interfaces;
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Infrastructure;

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
                .Include(ci => ci.Punches)
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

        // public async Task<IEnumerable<Punch>> GetChecklistItemsWithPunches(Guid checklistItemId, CancellationToken cancellationToken = default)
        // {
        //     var punches = await GetSet()
        //         .Include(ci => ci.Punches)
        //         .Where(ci => ci.ChecklistId == checklistId && ci.Punches.Count > 0)
        //         .SelectMany(ci => ci.Punches)
        //         //.Select(p => _mapper.Map<Pun)
        //         //.ProjectTo<PunchDto>(_mapper.ConfigurationProvider)
        //         .ToListAsync(cancellationToken);

        //     return punches;
        //     //.ToListAsync(cancellationToken)
        // }

        public async Task DeleteChecklistItem(Guid Id, CancellationToken cancellationToken = default)
        {
            await DeleteById(Id, cancellationToken);
        }
    }
}
