using System.Text.Json.Serialization;
using Application.Common;
using Application.Templates.Models;
using Domain.Entities.TemplateAggregate;
using Mapster;
using MobDeMob.Domain.Common;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates;

public class ItemTemplateDto : IRegister
{
    public string Id { get; init; }
    public ChecklistTemplateDto? ChecklistTemplate { get; private set; }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ItemTemplate, ItemTemplateDto>();
        // .Map(dest => dest.Questions, src => src.Questions.Select(q => q.Question)).Compile();
    }
}