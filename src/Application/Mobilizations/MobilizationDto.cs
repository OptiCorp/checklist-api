using Application.Common;
using Mapster;
using MobDeMob.Domain.Entities;

namespace Application.Mobilizations.Dtos;

public class MobilizationDto : DtoExtension, IRegister
{
    public required string Title { get; init; }
    public string Description { get; init; } = string.Empty;

    public MobilizationType Type { get; init; } 

    public MobilizationStatus Status { get; init; }

    //public int PartsCount { get; init; }

    public int ChecklistCountDone { get; init; }

    public int ChecklistCount { get; init; }

    //public double CompletionPercentage {get; init;}

    // public IList<string> PartIds {get; set;} = [];

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Mobilization, MobilizationDto>();
    }

}
