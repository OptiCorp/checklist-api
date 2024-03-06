using Application.Common;
using Mapster;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Domain.Entities.TemplateAggregate;

public class QuestionTemplateDto : IRegister
{
    // public Guid ItemTemplateId {get; private set;}
    // public ItemTemplate ItemTemplate {get;} = null!;
    public string Question { get; init; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<QuestionTemplate, QuestionTemplateDto>();
    }
}
