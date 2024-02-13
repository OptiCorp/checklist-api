using MediatR;

namespace Application.Templates.AddTemplate;

public class AddTemplateCommand : IRequest<Guid>
{
    public string ItemId { get; init; }
    public string? ItemName { get; init; }

    public string? ItemDescription { get; init; }

    public string? Revision { get; init; }

    public string? Type { get; set; }

    public IEnumerable<string>? Questions { get; set; }
}
