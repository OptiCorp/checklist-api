

using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Common.Interfaces;

public interface IChecklistRepository
{
    Task<string> AddChecklist(Checklist checklist, CancellationToken cancellationToken = default);

    //Task CreatePartChecklistQuestions(string id, List<string> questions, CancellationToken cancellationToken = default);

    //Task<IEnumerable<ChecklistSectionTemplate>?> GetQuestions(string id, CancellationToken cancellationToken = default);
}
