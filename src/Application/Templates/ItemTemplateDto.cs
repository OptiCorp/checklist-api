using System.Text.Json.Serialization;
using Application.Common;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates;

public class ItemTemplateDto : IRegister
{
    public Guid Id {get; init;}
    public IEnumerable<string> Questions { get; set; } = [];

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ItemTemplate, ItemTemplateDto>()
            .Map(dest => dest.Questions, src => src.Questions.Select(q => q.Question)).Compile();
    }
}