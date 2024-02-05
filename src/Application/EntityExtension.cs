
using MobDeMob.Application.Parts;
using MobDeMob.Application.Templates;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Entities.Mobilization;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Mobilizations;

public static class EntityExtenstions
{
    public static MobilizationDto AsDto(this Mobilization mobilization)
    {
        return new MobilizationDto{
            Title = mobilization.Title,
            Id = mobilization.Id,
            Description = mobilization.Description
        };
    }

    public static PartDto AsDto(this Part part)
    {
        return new PartDto(part.Type)
        {
             WpId = part.WpId,
             Name = part.Name,
             SerialNumber = part.SerialNumber,
             Children = part.Children,
             PartTemplateId = part.PartTemplateId,
        };
    }

    public static ChecklistSectionTemplateDto AsDto(this ChecklistSectionTemplate checklistSectionTemplate)
    {
        return new ChecklistSectionTemplateDto
        {
            ChecklistQuestion = checklistSectionTemplate.ChecklistQuestion,
            Id = checklistSectionTemplate.Id
        };
    }

}