
using Application.Mobilizations.Dtos;
using MobDeMob.Domain.Entities;

namespace MobDeMob.Application.Mobilizations;

// TODO change this to mapster/automapper OR
// bring this logic to the constructor of the dto
// the goal is to avoid mixing logic from different features/entities
// in case one feature has to be changed/removed and you're working on a big team, this will: 1) reduce merge conflicts 2) speed up code reviews 3) make unit testing easier
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

    //private static PartChildDto AsChildDto(this Part part)
    //{
    //    return new PartChildDto(part.Type)
    //    {
    //        partChildId = part.Id,
    //    };
    //}

    //public static PartDto AsDto(this Part part)
    //{
    //    return new PartDto(part.Type)
    //    {
    //        PartId = part.Id,
    //        WpId = part.WpId,
    //        Name = part.Name,
    //        SerialNumber = part.SerialNumber,
    //        hasChecklistTemplate = part.hasChecklistTemplate,
    //        //Children = part.Children.Select(c => c.AsDto()),
    //        Children = part.Children.Select(c => c.AsChildDto()),
    //        PartTemplateId = part.PartTemplateId,
    //    };
    //}

    //public static PunchDto AsDto(this Punch punch, string? blobURIWithSAS)
    //{
    //    return new PunchDto
    //    {
    //        Id = punch.Id,
    //        Title = punch.Title,
    //        ChecklistId = punch.Part.ChecklistId,
    //        PartId = punch.Part.Id,
    //        Description = punch.Description,
    //        ImageBlobUri = blobURIWithSAS
    //    };
    //}

    //public static ChecklistSectionTemplateDto AsDto(this ChecklistSectionTemplate checklistSectionTemplate)
    //{
    //    return new ChecklistSectionTemplateDto
    //    {
    //        ChecklistQuestion = checklistSectionTemplate.ChecklistQuestion,
    //        Id = checklistSectionTemplate.Id
    //    };
    //}

}
