using Application.Templates;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Punches.Dtos;

public class PunchListDto
{

    public string? SASToken {get; set;}

    public Guid ChecklistItemId {get; init;}

    public IEnumerable<Guid> PunchIds {get; init;} = [];

    public ItemTemplateDto ItemTemplate {get; init;}

    public PunchListDto(IEnumerable<Guid> punchIds, ItemTemplateDto itemTemplate, Guid checklistItemId, string? sasToken)
    {
        PunchIds = punchIds;
        ItemTemplate = itemTemplate;
        ChecklistItemId = checklistItemId;
        SASToken = sasToken;
    }

    // public void Mapping(Profile profile)
    // {
    //     profile.CreateMap<IEnumerable<PunchDto>, PunchListDto>();
    //         //.ForMember(pl => pl.Punches, opt => opt.MapFrom(p => p))
    // }
}