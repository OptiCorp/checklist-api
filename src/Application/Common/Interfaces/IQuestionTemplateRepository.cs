
using Domain.Entities.TemplateAggregate;

namespace Application.Common.Interfaces;

public interface IQuestionTemplateRepository
{
    public Task<QuestionTemplate?> GetSingleQuestion (Guid questionId, CancellationToken cancellationToken = default);

    public Task<Guid> AddQuestion (string question, CancellationToken cancellationToken = default);

    public Task DeleteQuestionById (Guid Id, CancellationToken cancellationToken = default);

    Task SaveChanges(CancellationToken cancellationToken = default);


}