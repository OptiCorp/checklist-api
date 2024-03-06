
using Application.Common;
using Application.Punches.Dtos;
using Application.Templates;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MobDeMob.Domain.Enums;

namespace Application.Checklists.Dtos;

public class ChecklistBriefDto : DtoExtension, IRegister
{
    public string ItemId { get; init; }

    //public Checklist Checklist {get; set;}

    //public Guid ItemTemplateId { get; init; }
    public Guid MobilizationId {get; init;}

    public int? PunchesCount {get; init;}

    //public ItemTemplateBriefDto Template { get; init; } = null!;

    //public ICollection<PunchDto> Punches { get; init; } = [];
    public ChecklistStatus Status { get; init; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Checklist, ChecklistBriefDto>();
    }
}