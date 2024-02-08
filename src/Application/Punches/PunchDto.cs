using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class PunchDto
{

    public string? Id {get; set;}
    public required string Title { get; set; }

    public string? Description { get; set; }

    public string? ImageBlobUri {get; set;}

    public string? PartId {get; set;}

    public required string ChecklistId {get; set;}
}