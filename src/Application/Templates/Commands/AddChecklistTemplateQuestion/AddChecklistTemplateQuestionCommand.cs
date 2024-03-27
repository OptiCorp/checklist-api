using MediatR;

namespace Application.Templates.Commands;

public class AddChecklistTemplateQuestionCommand : IRequest<Guid>
{
    public Guid checklistTemplateId { get; init; }
    public string question {get; init;}

    public QuestionTemplateAddConflictOptions? conflictOption {get; init;}
}
