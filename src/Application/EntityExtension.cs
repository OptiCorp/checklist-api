
using Application.Mobilizations.Dtos;
using Application.Punches.Dtos;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace MobDeMob.Application.Mobilizations;

// TODO change this to mapster/automapper OR
// bring this logic to the constructor of the dto
// the goal is to avoid mixing logic from different features/entities
// in case one feature has to be changed/removed and you're working on a big team, this will: 1) reduce merge conflicts 2) speed up code reviews 3) make unit testing easier
public static class EntityExtenstions
{
    public static MobilizationDto AsDto(this Mobilization mobilization)
    {
        return new MobilizationDto
        {
            Title = mobilization.Title,
            Id = mobilization.Id,
            Description = mobilization.Description,
            Type = mobilization.Type
        };
    }

    public static PunchDto AsDto(this Punch punch, Uri? SASToken = null)
    {
       return new PunchDto
       {
           Id = punch.Id,
           Title = punch.Title,
           //PartId = punch.ChecklistItem.ItemId,
           Description = punch.Description,
           //ChecklistItemId = punch.ChecklistItemId,
           //ImageBlobUris = punch.ImageBlobUris,
           //SASToken = SASToken,
       };
    }
}
