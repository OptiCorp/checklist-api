using MobDeMob.Domain.Common;

namespace Domain.Entities.TemplateAggregate;

public class QuestionTemplate : AuditableEntity
{
    public required string Question { get; set; }
}
