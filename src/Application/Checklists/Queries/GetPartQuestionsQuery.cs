
using MediatR;
using MobDeMob.Application.Templates;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Parts.Queries
{
    public class GetPartQuestionsQuery : IRequest<IEnumerable<ChecklistSectionTemplateDto>>
    {
        public required string Id { get; init; }
    }
}