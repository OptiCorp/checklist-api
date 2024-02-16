using MediatR;
using MobDeMob.Domain.Entities;

namespace Application.Mobilizations.Commands;

public class AddMobilizationCommand : IRequest<Guid>
{
    public required string Title {get; init;}

    public string? Description {get; init;}

    public MobilizationType MobilizationType {get; set;}
}
