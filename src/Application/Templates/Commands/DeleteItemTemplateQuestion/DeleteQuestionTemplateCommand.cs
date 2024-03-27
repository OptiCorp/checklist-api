using MediatR;

namespace Application.Templates.Commands;

public class DeleteQuestionTemplateCommand : IRequest
{
    public Guid QuestionTemplateId { get; init; }

}
