using System.Text.Json.Serialization;
using Application.Common;
using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;

namespace Application.Templates;

public class ItemTemplateDto : DtoExtension
{
    public required string ItemId { get; set; }

    public required string Name { get; set; }

    public required string Type { get; set; }

    public string? Revision { get; set; }

    public string? Description { get; set; }

    // public ItemTemplateDto? ParentItemTemplate {get; set;}
    // public ICollection<ItemTemplateDto> Children {get; set;} = [];

    public IEnumerable<QuestionTemplate> Questions { get; set; } = [];

    public ItemTemplateDto(IEnumerable<QuestionTemplate> questions)
    {
        Questions = questions;
    }
}