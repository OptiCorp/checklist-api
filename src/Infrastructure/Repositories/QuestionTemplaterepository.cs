
using Application.Common.Interfaces;
using Domain.Entities.TemplateAggregate;
using Infrastructure.Repositories.Common;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories;

public class QuestionTemplateRepository : RepositoryBase<QuestionTemplate>, IQuestionTemplateRepository
{
    public QuestionTemplateRepository(ModelContextBase modelContextBase) : base(modelContextBase)
    { 
    }

    public async Task<Guid> AddQuestion(string question, CancellationToken cancellationToken = default)
    {
        var newQuestion = QuestionTemplate.New(question);
        await Add(newQuestion, cancellationToken);
        return newQuestion.Id;
    }

    public async Task<QuestionTemplate?> GetSingleQuestion(Guid questionId, CancellationToken cancellationToken = default)
    {
        return await GetById(questionId, cancellationToken);
    }

    public async Task DeleteQuestionById(Guid Id, CancellationToken cancellationToken) 
    {
        await DeleteById(Id, cancellationToken);
    }
}