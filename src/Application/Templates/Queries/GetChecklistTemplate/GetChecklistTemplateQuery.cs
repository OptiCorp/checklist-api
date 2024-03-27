using Application.Templates.Models;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Queries;

public class GetChecklistTemplateQuery : IRequest<ChecklistTemplateDto?>
{
    public Guid checklistTemplateId { get; init; }
}
