using MediatR;

namespace Application.Checklists.Commands.AddItem;

public class AddItemCommand : IRequest<Guid>
{
    public Guid MobilizationId { get; set; }

    public required string ItemId { get; set; }
}
