using Application.Common.Interfaces;
using Domain.Entities.ChecklistAggregate;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Infrastructure;

namespace Infrastructure.Repositories
{
    public class ChecklistQuestionRepository : RepositoryBase<ChecklistQuestion>, IChecklistQuestionRepository
    {
        public ChecklistQuestionRepository(ModelContextBase modelContextBase) : base(modelContextBase) 
        { 
            
        }

        public async Task<Guid> AddQuestion(ChecklistQuestion question, CancellationToken cancellationToken = default)
        {
            await Add(question, cancellationToken);

            return question.Id;
        }

        public async Task<ChecklistQuestion?> GetQuestion(Guid Id, CancellationToken cancellationToken = default)
        => await GetById(Id, cancellationToken);

        public async Task<IEnumerable<ChecklistQuestion>> GetQuestionsByChecklistId(Guid Id, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Include(c => c.QuestionTemplate)
                .Where(c => c.ChecklistId == Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ChecklistQuestion>> GetQuestionsByQuestionTemplateId(Guid questionTemplateId, CancellationToken cancellationToken = default)
        {
            return await GetSet()
                .Where(cq => cq.QuestionTemplateId == questionTemplateId)
                .ToListAsync(cancellationToken);
        }
    }
}
