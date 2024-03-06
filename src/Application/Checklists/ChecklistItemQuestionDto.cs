using Application.Common;
using Application.Common.Mappings;
using Domain.Entities.ChecklistAggregate;
using Domain.Entities.TemplateAggregate;
using TypeAdapterConfig = Mapster.TypeAdapterConfig;
using MobDeMob.Domain.Common;
using Mapster;

namespace Application.Checklists.Dtos;

public class ChecklistQuestionDto : DtoExtension, IRegister
{
    public Guid ChecklistId { get; set; }

    public Guid QuestionTemplateId { get; set; }

    public bool Checked { get; set; }

    public bool NotApplicable { get; set; }

    public string Question { get; set; } = string.Empty;

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ChecklistQuestion, ChecklistQuestionDto>();
    }

}
