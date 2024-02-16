using MediatR;

namespace Application.Templates.AddTemplate;

public class AddTemplateCommand : IRequest<Guid>
{
    public required string ItemId { get; init; }
    public required string ItemName { get; init; }

    public string? ItemDescription { get; init; }

    public string? Revision { get; init; }

    public required string Type { get; set; }

    public IEnumerable<string>? Questions { get; set; }
}
