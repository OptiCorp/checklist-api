using MediatR;

namespace Application.Templates.UpdateTemplate;
public class UpdateTemplateCommand : IRequest
{
    //public string ItemId { get; init; }
    public Guid QuestionTemplateId {get; init;}
    public string Question { get; init; }
}
