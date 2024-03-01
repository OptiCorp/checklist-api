using MediatR;

namespace MobDeMob.Application.Mobilizations.Commands;

public class RemoveItemFromMobilizationCommand : IRequest
{
    public Guid Id {get; init;}
    public string ItemId {get; init;}

}