using MediatR;

namespace Application.Templates.AddTemplate;

public class AddChecklistTemplateCommand : IRequest<ItemTemplateDto>
{
    public string itemTemplateId { get; init; }
    public IEnumerable<string> Questions { get; set; }
}
