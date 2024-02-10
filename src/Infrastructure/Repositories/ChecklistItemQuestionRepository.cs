using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using Infrastructure.Repositories.Common;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories
{
    public class ChecklistItemQuestionRepository : RepositoryBase<ChecklistItemQuestion>, IChecklistItemQuestionRepository
    {
        public ChecklistItemQuestionRepository(ModelContextBase modelContextBase) : base(modelContextBase) { }

        public async Task<string> AddQuestion(ChecklistItemQuestion question, CancellationToken cancellationToken = default)
        {
            await Add(question, cancellationToken);

            return question.Id;
        }
    }
}
