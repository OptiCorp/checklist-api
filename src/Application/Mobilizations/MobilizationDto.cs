using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations;

public record MobilizationDto
{
    public string? Id {get; init;}
    public required string Title {get; init;}
    public string? Description {get; init;}

    public string MobilizationType {get; init;}

    public MobilizationDto(MobilizationType mobilizationType)
    {
        MobilizationType = mobilizationType.ToString();
    }
}