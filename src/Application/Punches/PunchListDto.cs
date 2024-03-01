using Application.Templates;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Punches.Dtos;

public class PunchListDto
{

    //public string? SASToken {get; init;}

    public Guid ChecklistId {get; init;}

    public IEnumerable<Guid> PunchIds {get; init;} = [];

    public string ItemId {get; init;}

    //public ItemTemplateDto ItemTemplate {get; init;}

    public PunchListDto(IEnumerable<Guid> punchIds, Guid checklistId, string itemId)
    {
        PunchIds = punchIds;
        ChecklistId = checklistId;
        ItemId = itemId;
        //SASToken = sasToken;
    }

    // public void Mapping(Profile profile)
    // {
    //     profile.CreateMap<IEnumerable<PunchDto>, PunchListDto>();
    //         //.ForMember(pl => pl.Punches, opt => opt.MapFrom(p => p))
    // }
}