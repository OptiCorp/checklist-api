using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class AddMobilizationCommand : IRequest<string>
{
    public required string Title {get; init;}

    public string? Description {get; init;}

}
