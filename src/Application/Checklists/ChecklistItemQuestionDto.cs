using Application.Common;
using Application.Common.Mappings;
using Domain.Entities.ChecklistAggregate;
using Domain.Entities.TemplateAggregate;
using TypeAdapterConfig = Mapster.TypeAdapterConfig; 
using MobDeMob.Domain.Common;
using Mapster;

namespace Application.Checklists.Dtos;

public class ChecklistItemQuestionDto : DtoExtension, IRegister
{
    public Guid ChecklistItemId { get; set; }

    public Guid QuestionTemplateId { get; set; }

    public bool Checked { get; set; } 

    // public void Mapping(TypeAdapterConfig config)
    // {
    //     config.NewConfig<ChecklistItemQuestion, ChecklistItemQuestionDto>();
    // }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ChecklistItemQuestion, ChecklistItemQuestionDto>();
    }

}
