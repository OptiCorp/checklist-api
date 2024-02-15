using Application.Common.Mappings;
using AutoMapper;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace Application.Punches.Dtos;

public class PunchListDto
{

    public string? SASToken {get; set;}

    public IEnumerable<PunchDto> Punches {get; init;} = [];

    public PunchListDto(IEnumerable<PunchDto> punches)
    {
        Punches = punches;
    }

    // public void Mapping(Profile profile)
    // {
    //     profile.CreateMap<IEnumerable<PunchDto>, PunchListDto>();
    //         //.ForMember(pl => pl.Punches, opt => opt.MapFrom(p => p))
    // }
}