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
    public class ChecklistRepository : RepositoryBase<Checklist>, IChecklistRepository
    {
        public ChecklistRepository(ModelContextBase modelContextBase) : base(modelContextBase)
        {

        }

        public async Task<Guid> AddChecklist(Checklist checklist, CancellationToken cancellationToken = default)
        {
            await Add(checklist, cancellationToken);

            return checklist.Id;
        }

        //I dont think we need this or that this makes sense
        public async Task<Checklist?> GetChecklistByItemId(string itemId, Guid checklistCollectionId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Where(ci => ci.ChecklistCollectionId == checklistCollectionId)
                .Include(ci => ci.ItemTemplate)
                .SingleOrDefaultAsync(ci => ci.ItemTemplate.ItemId == itemId, cancellationToken);
        }

        public async Task<IEnumerable<Checklist>> GetChecklistsForChecklistCollection(Guid checklistCollectionId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Include(ci => ci.ItemTemplate)
                .Where(ci => ci.ChecklistCollectionId == checklistCollectionId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Checklist?> GetSingleChecklist(Guid checklistId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                // .Include(c => c.Questions)
                // .ThenInclude(c => c.QuestionTemplate)
                .Include(c => c.ItemTemplate)
                .SingleOrDefaultAsync(c => c.Id == checklistId, cancellationToken);
        }

        public async Task DeleteChecklistById(Guid Id, CancellationToken cancellationToken = default)
        {
            await DeleteById(Id, cancellationToken);
        }

        // public void RemoveChecklistItem(ChecklistItem checklistItem, CancellationToken cancellationToken = default)
        // {
        //     Remove(checklistItem);
        // }

        public async Task<Checklist?> GetChecklistWithTemplate(Guid checklistId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                 .Include(c => c.ItemTemplate)
                 //.Include(c => c.Template)
                 .SingleOrDefaultAsync(c => c.Id == checklistId, cancellationToken);
        }

        public async Task<PaginatedList<Checklist>> GetChecklistsWithPaginationFromChecklistCollection(Guid checklistCollectionId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Where(ci => ci.ChecklistCollectionId == checklistCollectionId)
                .Include(ci => ci.ItemTemplate)
                .OrderBy(x => x.Created)
                .PaginatedListAsync(pageNumber, pageSize);
        }

        public async Task<PaginatedList<Checklist>> GetChecklistsForItem(string ItemId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Include(c => c.ItemTemplate)
                .Include(c => c.Punches)
                .Where(m => m.ItemTemplate.ItemId == ItemId)
                .PaginatedListAsync(pageNumber, pageSize);
        }

        //TODO: I think this can be no tracking
        public async Task<IEnumerable<Checklist>> GetChecklistByItemTemplateId(Guid itemTemplateId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Where(c => c.ItemTemplateId == itemTemplateId)
                .ToListAsync(cancellationToken);
        }
    }
}
