using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class RemovePartFromMobilizationCommand : IRequest
{
    public Guid Id {get; init;}
    public required string PartId {get; init;}

}