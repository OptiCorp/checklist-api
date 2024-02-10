using Domain.Entities.ChecklistAggregate;

namespace Application.Common.Interfaces
{
    public interface IChecklistItemQuestionRepository
    {
        Task<string> AddQuestion(ChecklistItemQuestion question, CancellationToken cancellationToken = default);
    }
}
