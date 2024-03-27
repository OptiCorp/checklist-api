

using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Common.Interfaces;

public interface IChecklistCollectionRepository
{
    Task<Guid> AddChecklistCollection(ChecklistCollection checklistCollection, CancellationToken cancellationToken = default);

    Task DeleteChecklistCollection(Guid id, CancellationToken cancellationToken = default);

    //void RemovePartFromChecklist(ChecklistCollection checklist, string partId);

    Task SaveChanges(CancellationToken cancellationToken = default);

    //Task CreatePartChecklistQuestions(string id, List<string> questions, CancellationToken cancellationToken = default);

    //Task<IEnumerable<ChecklistSectionTemplate>?> GetQuestions(string id, CancellationToken cancellationToken = default);
}
