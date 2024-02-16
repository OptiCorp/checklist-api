using MediatR;

namespace Application.Checklists.Commands.AddPunch;

public class AddPunchCommand : IRequest<Guid>
{
    public Guid MobilizationId { get; set; }
    public required string ItemId { get; set; }

    public required string Title {get; init;}

    public string Description {get; init;}
}
