using MobDeMob.Domain.Entities;

namespace Application.Mobilizations.Dtos;

public record MobilizationDto
{
    public string? Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }

    public MobilizationType MobilizationType { get; init; }

    public MobilizationDto(MobilizationType mobilizationType)
    {
        MobilizationType = mobilizationType;
    }
}
