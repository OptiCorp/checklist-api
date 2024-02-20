using Application.Common.Mappings;
using AutoMapper;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Punches.Dtos;

public class PunchListDto
{

    public string? SASToken {get; set;}

    public IEnumerable<PunchDto> Punches {get; init;} = [];

    public ItemTemplate ItemTemplate {get; init;}

    public PunchListDto(IEnumerable<PunchDto> punches, ItemTemplate itemTemplate)
    {
        Punches = punches;
        ItemTemplate = itemTemplate;
    }

    // public void Mapping(Profile profile)
    // {
    //     profile.CreateMap<IEnumerable<PunchDto>, PunchListDto>();
    //         //.ForMember(pl => pl.Punches, opt => opt.MapFrom(p => p))
    // }
}