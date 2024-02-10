using MediatR;

namespace Application.Checklists.Commands.AddItem;

public class AddItemCommand : IRequest<string>
{
    public string MobilizationId { get; set; }

    public string ItemId { get; set; }
}
