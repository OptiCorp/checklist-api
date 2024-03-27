
using Application.Common;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.Models;

public class ChecklistTemplateDto : DtoExtension, IRegister
{
    public string ItemTemplateId { get; init; }

    public ICollection<QuestionTemplate> Questions { get; init; } = [];

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ChecklistTemplate, ChecklistTemplateDto>();
    }
}