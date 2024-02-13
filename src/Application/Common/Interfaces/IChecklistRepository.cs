

using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Common.Interfaces;

public interface IChecklistRepository
{
    Task<Guid> AddChecklist(Checklist checklist, CancellationToken cancellationToken = default);

    Task DeleteChecklist(Guid id, CancellationToken cancellationToken);

    //Task CreatePartChecklistQuestions(string id, List<string> questions, CancellationToken cancellationToken = default);

    //Task<IEnumerable<ChecklistSectionTemplate>?> GetQuestions(string id, CancellationToken cancellationToken = default);
}
