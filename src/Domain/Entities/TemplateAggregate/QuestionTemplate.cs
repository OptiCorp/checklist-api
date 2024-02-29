using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Domain.Entities.TemplateAggregate;

public class QuestionTemplate : AuditableEntity
{
    public Guid ItemTemplateId {get; set;}
    public ItemTemplate ItemTemplate {get;} = null!;
    public required string Question { get; set; }
}
