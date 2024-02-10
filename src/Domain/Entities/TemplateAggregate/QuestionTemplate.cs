using MobDeMob.Domain.Common;

namespace Domain.Entities.TemplateAggregate;

public class QuestionTemplate : AuditableEntity
{
    public string Question { get; set; }
}
