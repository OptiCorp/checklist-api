using System.Text.Json.Serialization;
using Application.Common;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates;

public class ItemTemplateBriefDto : DtoExtension, IRegister
{
    public required string ItemId { get; set; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ItemTemplate, ItemTemplateBriefDto>();
    }
}