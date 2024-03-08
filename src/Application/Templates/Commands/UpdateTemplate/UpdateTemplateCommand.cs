using MediatR;

namespace Application.Templates.UpdateTemplate;
public class UpdateTemplateCommand : IRequest
{
    public string ItemId { get; init; }
    public IEnumerable<string>? Questions { get; init; }
}
