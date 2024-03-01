using MediatR;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations.Commands;

public class UpdateMobilizationCommand : IRequest
{
    public Guid Id {get; init;}
    public required string Title {get; init;}
    public string? Description {get; init;}

    public MobilizationStatus Status {get; set;}

}
