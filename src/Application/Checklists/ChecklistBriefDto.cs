
using Application.Common;
using Application.Punches.Dtos;
using Application.Templates;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MobDeMob.Domain.Enums;

namespace Application.Checklists.Dtos;

public class ChecklistBriefDto : DtoExtension, IRegister
{
    public required string ItemId { get; init; }

    public Guid ChecklistId { get; init; }

    //public Checklist Checklist {get; set;}

    public Guid TemplateId { get; init; }

    public ItemTemplateBriefDto Template { get; init; } = null!;

    //public ICollection<PunchDto> Punches { get; init; } = [];
    public ChecklistStatus Status { get; init; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Checklist, ChecklistBriefDto>();
    }
}