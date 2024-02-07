
using MobDeMob.Application.Parts;
using MobDeMob.Application.Templates;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Application.Mobilizations;

public static class EntityExtenstions
{
    public static MobilizationDto AsDto(this Mobilization mobilization)
    {
        return new MobilizationDto(mobilization.Type)
        {
            Title = mobilization.Title,
            Id = mobilization.Id,
            Description = mobilization.Description,
        };
    }

    private static PartChildDto AsChildDto(this Part part)
    {
        return new PartChildDto(part.Type)
        {
            partChildId = part.Id,
        };
    }

    public static PartDto AsDto(this Part part)
    {
        return new PartDto(part.Type)
        {
            PartId = part.Id,
            WpId = part.WpId,
            Name = part.Name,
            SerialNumber = part.SerialNumber,
            hasChecklistTemplate = part.hasChecklistTemplate,
            //Children = part.Children.Select(c => c.AsDto()),
            Children = part.Children.Select(c => c.AsChildDto()),
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