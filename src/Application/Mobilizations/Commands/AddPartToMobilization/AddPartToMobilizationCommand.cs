using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class AddPartToMobilizationCommand : IRequest
{
    public required string id {get; init;}
    public required string partId {get; init;}

}