using MediatR;

namespace Application.Templates.AddTemplate;

public class AddTemplateCommand : IRequest<Guid>
{
    public required string ItemId { get; init; }
    public IEnumerable<string>? Questions { get; set; }
}
