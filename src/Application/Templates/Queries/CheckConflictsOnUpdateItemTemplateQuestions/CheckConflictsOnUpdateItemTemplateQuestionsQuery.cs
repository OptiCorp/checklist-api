using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Queries;

public class CheckConflictsOnUpdateItemTemplateQuestionsQuery : IRequest<IEnumerable<Guid>>
{
    public Guid ItemTemplateId {get; init;}
}
