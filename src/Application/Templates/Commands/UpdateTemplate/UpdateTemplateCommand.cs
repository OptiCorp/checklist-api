using MediatR;

namespace Application.Templates.UpdateTemplate;
public class UpdateTemplateCommand : IRequest
{
    //public string ItemId { get; init; }
    public Guid questionTemplateId {get; init;}
    public string question { get; init; }
}
