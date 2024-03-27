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

        public async Task<IEnumerable<Checklist>> GetChecklistsByItemId(string itemId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Where(ci => ci.ItemId == itemId)
                // .ThenInclude(itt => itt.ItemTemplate)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Checklist>> GetChecklistsForChecklistCollection(Guid checklistCollectionId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Include(ci => ci.Item)
                .Where(ci => ci.ChecklistCollectionId == checklistCollectionId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Checklist?> GetSingleChecklist(Guid checklistId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                // .Include(c => c.Questions)
                // .ThenInclude(c => c.QuestionTemplate)
                .Include(c => c.Item)
                .ThenInclude(i => i.ItemTemplate)
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

        // public async Task<Checklist?> GetChecklistWithTemplate(Guid checklistId, CancellationToken cancellationToken = default)
        // {
        //     return await GetSet()
        //          .Include(c => c.ItemTemplate)
        //          //.Include(c => c.Template)
        //          .SingleOrDefaultAsync(c => c.Id == checklistId, cancellationToken);
        // }

        public async Task<PaginatedList<Checklist>> GetChecklistsWithPaginationFromChecklistCollection(Guid checklistCollectionId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Where(ci => ci.ChecklistCollectionId == checklistCollectionId)
                .Include(ci => ci.Item)
                .OrderBy(c => c.Created)
                .PaginatedListAsync(pageNumber, pageSize);
        }

        public async Task<PaginatedList<Checklist>> GetChecklistsForItem(string itemId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Include(c => c.Punches)
                .Where(m => m.ItemId == itemId)
                .PaginatedListAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<Checklist>> GetChecklistsByChecklistTemplateId(Guid checklistTemplateId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Where(c => c.ChecklistTemplateId == checklistTemplateId)
                .ToListAsync(cancellationToken);
        }

        public async Task<PaginatedList<Checklist>> GetChecklistsForItemBySearch(string itemId, string checklistSearchId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .OrderBy(c => c.ItemId)
                .Where(c => c.ItemId == itemId)
                .Where(c => c.Id.ToString().Contains(checklistSearchId))
                .PaginatedListAsync(pageNumber, pageSize);
        }


        // public async Task<IEnumerable<Checklist>> GetChecklistsByItemTemplateId(Guid itemTemplateId, CancellationToken cancellationToken = default)
        // {
        //     return await GetSet()
        //         .Where(c => c.ItemTemplateId == itemTemplateId)
        //         .ToListAsync(cancellationToken);
        // }
    }
}
