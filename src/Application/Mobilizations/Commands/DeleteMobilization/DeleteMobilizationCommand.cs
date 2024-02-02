using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class DeleteMobilizationCommand : IRequest
{
    public required string id {get; init;}
}
