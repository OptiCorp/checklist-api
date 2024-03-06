using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.Entities.ChecklistAggregate;

public class ChecklistCollection : AuditableEntity
{
    public Mobilization Mobilization { get; set; } = null!;

    public ICollection<Checklist> Checklists {get; set;} = [];


    [NotMapped]
    public int ChecklistsCountDone => Checklists.Count(ci => ci.Status == Enums.ChecklistStatus.Completed);

    [NotMapped]
    public int ChecklistsCount => Checklists.Count;

}
