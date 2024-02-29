using Application.Checklists.Dtos;
using Application.Common.Models;
using Application.Punches.Dtos;
using Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace Application.Common.Interfaces
{
    public interface IChecklistRepository
    {
        Task<Guid> AddChecklist(Checklist checklist, CancellationToken cancellationToken = default);

        Task<Checklist?> GetChecklistByItemId (string itemId, Guid checklistCollectionId, CancellationToken cancellationToken = default);

        // Task<IEnumerable<Punch>> GetChecklistItemsWithPunches(Guid checklistItemId, CancellationToken cancellationToken = default);

        Task<IEnumerable<Checklist>> GetChecklistsForChecklistCollection(Guid checklistCollectionId, CancellationToken cancellationToken = default);

        Task<PaginatedList<ChecklistBriefDto>> GetChecklistsWithPaginationFromChecklistCollection(Guid checklistCollectionId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);


        Task<Checklist?> GetSingleChecklist(Guid checklistId, CancellationToken cancellationToken = default);

        Task DeleteChecklistById(Guid Id, CancellationToken cancellationToken = default);

        Task<Checklist?> GetChecklistWithTemplate(Guid checklistId, CancellationToken cancellationToken = default);

        //void RemoveChecklist(Checklist checklist, CancellationToken cancellationToken = default);


        Task SaveChanges(CancellationToken cancellationToken);
    }
}
