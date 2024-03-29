using MediatR;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations.Commands;

public class AddMobilizationCommand : IRequest<string>
{
    public required string Title {get; init;}

    public string? Description {get; init;}

    public MobilizationType MobilizationType {get; set;}
}
