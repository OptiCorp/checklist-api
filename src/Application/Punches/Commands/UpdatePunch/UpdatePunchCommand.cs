using MediatR;

namespace Application.Punches.Commands;

public class UpdatePunchCommand : IRequest
{
    public Guid Id {get; init;}

    public required string Title {get; init;}

    public string? Description {get; init;}
}