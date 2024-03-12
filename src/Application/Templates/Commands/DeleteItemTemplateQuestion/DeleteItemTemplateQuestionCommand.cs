using MediatR;

namespace Application.Templates;

public class DeleteItemTemplateQuestionCommand : IRequest
{
    public Guid TemplateQuestionId { get; init; }

}
