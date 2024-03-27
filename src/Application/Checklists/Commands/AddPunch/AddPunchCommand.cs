using MediatR;

namespace Application.Checklists.Commands.AddPunch;

public class AddPunchCommand : IRequest<Guid>
{
    public required string itemId { get; init; }
    public Guid checklistId {get; init;}
    public required string title {get; init;}
    public string description {get; init;}
}
