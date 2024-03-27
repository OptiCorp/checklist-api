using Domain.Entities.ChecklistAggregate;

namespace Application.Common.Interfaces
{
    public interface IChecklistQuestionRepository
    {
        Task<Guid> AddQuestion(ChecklistQuestion question, CancellationToken cancellationToken = default);

        Task<ChecklistQuestion?> GetQuestion (Guid Id, CancellationToken cancellationToken = default);

        Task<IEnumerable<ChecklistQuestion>> GetQuestionsByChecklistId (Guid Id, CancellationToken cancellationToken = default);

        Task<IEnumerable<ChecklistQuestion>> GetQuestionsByQuestionTemplateId (Guid questionTemplateId, CancellationToken cancellationToken = default);

        Task SaveChanges(CancellationToken cancellationToken = default);
    }
}
