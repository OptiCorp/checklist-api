using System.Text.Json.Serialization;
using Application.Common;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates;

public class ItemDto : IRegister
{
    public string Id {get; init;}
    public string ItemTemplateId {get; init;}
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ItemTemplate, ItemTemplateDto>();
            // .Map(dest => dest.Questions, src => src.Questions.Select(q => q.Question)).Compile();
    }
}