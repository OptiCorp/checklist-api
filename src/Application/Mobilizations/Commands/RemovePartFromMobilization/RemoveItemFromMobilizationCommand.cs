using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class RemoveItemFromMobilizationCommand : IRequest
{
    public Guid Id {get; init;}
    public required string ItemId {get; init;}

}