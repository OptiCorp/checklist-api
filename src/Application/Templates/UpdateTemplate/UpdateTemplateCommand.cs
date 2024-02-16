using MediatR;

namespace Application.Templates.UpdateTemplate;

//TODO: should maybe be ItemId instead of templateId
public class UpdateTemplateCommand : IRequest
{
    public Guid Id { get; init; }

    public string? ItemName { get; init; }

    public string? ItemDescription { get; init; }

    public string? Revision { get; init; }

    public string? Type { get; set; }

    public IEnumerable<string>? Questions { get; set; }
}
