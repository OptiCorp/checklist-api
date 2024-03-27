using Application.Checklists.Dtos;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Queries;

public class CheckConflictsOnUpdateChecklistTemplateQuestionsQuery : IRequest<IEnumerable<ChecklistBriefDto>>
{
    public Guid checklistTemplateId {get; init;}
}
