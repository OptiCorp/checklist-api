using Application.Common;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.ChecklistAggregate;
using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;

namespace Application.Checklists.Dtos;

public class ChecklistItemQuestionDto : DtoExtension, IMapFrom<ChecklistItemQuestion>
{
    public Guid ChecklistItemId { get; set; }

    public Guid QuestionTemplateId { get; set; }

    public bool Checked { get; set; } 

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ChecklistItemQuestion, ChecklistItemQuestionDto>();
    }

}
