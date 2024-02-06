

using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Common.Interfaces;

public interface IChecklistRepository
{
    Task<string> AddChecklist(CancellationToken cancellationToken);

    Task CreatePartChecklistQuestions (string id, List<string> questions, CancellationToken cancellationToken);

    Task<IEnumerable<ChecklistSectionTemplate>?> GetQuestions (string id,CancellationToken cancellationToken);
}