
using MobDeMob.Domain.Entities.Mobilization;

namespace MobDeMob.Application.Mobilizations;

public static class EntityExtenstions
{
    public static MobilizationDto AsDto(this Mobilization mobilization)
    {
        return new MobilizationDto{
            Title = mobilization.Title,
            Id = mobilization.Id,
            Description = mobilization.Description
        };
    }
}