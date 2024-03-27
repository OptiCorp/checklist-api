
using Application.Common;
using Application.Punches.Dtos;
using Application.Templates;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MobDeMob.Domain.Enums;

namespace Application.Checklists.Dtos;

public class ChecklistBriefDto : IRegister
{
    public Guid Id {get; init;}
    public string ItemId { get; init; }

    public Guid MobilizationId {get; init;}

    public Guid ChecklistTemplateId {get; private set;}

    public int? PunchesCount {get; init;}

    public ChecklistStatus Status { get; init; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Checklist, ChecklistBriefDto>();
    }
}