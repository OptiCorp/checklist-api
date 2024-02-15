using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using Infrastructure.Repositories.Common;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories
{
    public class ChecklistItemQuestionRepository : RepositoryBase<ChecklistItemQuestion>, IChecklistItemQuestionRepository
    {
        public ChecklistItemQuestionRepository(ModelContextBase modelContextBase) : base(modelContextBase) { }

        public async Task<Guid> AddQuestion(ChecklistItemQuestion question, CancellationToken cancellationToken = default)
        {
            await Add(question, cancellationToken);

            return question.Id;
        }

        public async Task<ChecklistItemQuestion?> GetQuestion(Guid Id, CancellationToken cancellationToken = default)
        => await GetById(Id, cancellationToken);
    }
}
