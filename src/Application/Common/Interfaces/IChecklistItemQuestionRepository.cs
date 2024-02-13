using Domain.Entities.ChecklistAggregate;

namespace Application.Common.Interfaces
{
    public interface IChecklistItemQuestionRepository
    {
        Task<Guid> AddQuestion(ChecklistItemQuestion question, CancellationToken cancellationToken = default);
    }
}
