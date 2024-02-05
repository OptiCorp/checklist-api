using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class RemovePartFromMobilizationCommand : IRequest
{
    public required string id {get; init;}
    public required string partId {get; init;}

}