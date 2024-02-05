namespace MobDeMob.Application.Mobilizations;

public record MobilizationDto
{
    public string? Id {get; init;}
    public required string Title {get; init;}
    public string? Description {get; init;}
}