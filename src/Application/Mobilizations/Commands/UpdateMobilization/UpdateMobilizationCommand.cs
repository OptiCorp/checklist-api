using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class UpdateMobilizationCommand : IRequest
{
    public required string id {get; init;}
    public string? Title {get; init;}

    public string? Description {get; init;}

}
