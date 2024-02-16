using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class DeleteMobilizationCommand : IRequest
{
    public Guid Id {get; init;}
}
