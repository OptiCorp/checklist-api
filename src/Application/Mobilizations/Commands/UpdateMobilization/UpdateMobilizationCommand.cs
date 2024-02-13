using MediatR;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations.Commands;

public class UpdateMobilizationCommand : IRequest
{
    public Guid id {get; init;}
    public string? Title {get; init;}
    public string? Description {get; init;}
    public MobilizationType Type {get; set;}

    public MobilizationStatus Status {get; set;}

}
