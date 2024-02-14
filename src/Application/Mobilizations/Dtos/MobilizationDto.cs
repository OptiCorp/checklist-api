using Application.Common;
using Application.Common.Mappings;
using AutoMapper;
using MobDeMob.Domain.Entities;

namespace Application.Mobilizations.Dtos;

public class MobilizationDto : DtoExtension, IMapFrom<Mobilization>
{
    public required string Title { get; init; }
    public string? Description { get; init; }

    public MobilizationType Type { get; init; }

    public MobilizationStatus Status { get; set; }

    public IList<string> PartIds {get; set;} = [];

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Mobilization, MobilizationDto>();
    }
}
