
using Application.Common;
using Application.Punches.Dtos;
using Application.Templates;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MobDeMob.Domain.Enums;

namespace Application.Checklists.Dtos;

public class ChecklistDto : DtoExtension, IRegister
{
    public string ItemId { get; init; }

    //public Guid ChecklistCollectionId { get; init; }

    //public Checklist Checklist {get; set;}

    public Guid ItemTemplateId { get; init; }

    public ICollection<ChecklistQuestionDto> Questions { get; init; } = [];

    //public ICollection<PunchDto> Punches { get; init; } = [];
    public int? PunchesCount {get; init;}

    public ChecklistStatus Status { get; init; }

    public double CompletionPercentage { get; init; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Checklist, ChecklistDto>();
    }
}