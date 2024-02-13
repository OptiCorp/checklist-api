using MediatR;

namespace Application.Checklists.Commands.AddItem;

public class AddItemCommand : IRequest<Guid>
{
    public Guid MobilizationId { get; set; }

    public string ItemId { get; set; }
}
