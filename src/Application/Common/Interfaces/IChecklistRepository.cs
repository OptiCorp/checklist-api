

using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Common.Interfaces;

public interface IChecklistRepository
{
    Task<Guid> AddChecklist(Checklist checklist, CancellationToken cancellationToken = default);

    Task DeleteChecklist(Guid id, CancellationToken cancellationToken = default);

    void RemovePartFromChecklist(Checklist checklist, string partId);

    Task SaveChanges(CancellationToken cancellationToken = default);

    //Task CreatePartChecklistQuestions(string id, List<string> questions, CancellationToken cancellationToken = default);

    //Task<IEnumerable<ChecklistSectionTemplate>?> GetQuestions(string id, CancellationToken cancellationToken = default);
}
