
using Application.Common;
using Application.Punches.Dtos;
using Application.Templates;
using Domain.Entities.ChecklistAggregate;
using Mapster;
using MobDeMob.Domain.Enums;

namespace Application.Checklists.Dtos;

public class ChecklistItemDto : DtoExtension, IRegister
{
    public required string ItemId { get; init; }

    public Guid ChecklistId { get; init; }

    //public Checklist Checklist {get; set;}

    public Guid TemplateId { get; init; }

    public ItemTemplateDto Template { get; init; } = null!;

    public ICollection<ChecklistItemQuestionDto> Questions { get; init; } = [];

    //public ICollection<PunchDto> Punches { get; init; } = [];
    public int? PunchesCount {get; init;}

    public ChecklistItemStatus Status { get; init; }

    public double CompletionPercentage { get; init; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ChecklistItem, ChecklistItemDto>();
    }
}