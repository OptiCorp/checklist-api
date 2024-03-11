using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Queries;

public class GetTemplatesExistsFromItemIdsQuery : IRequest<IEnumerable<TemplateExistsReponse>>
{
    public IEnumerable<string> ItemIds { get; init; }

}
