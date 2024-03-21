using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Entities.TemplateAggregate;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.ItemAggregate;

public class ItemTemplate
{
    public string Id {get; set;}
    public ICollection<Item> Items { get; set; } = [];
    // public string ItemId { get; private set; }

    // public Guid? ParentItemTemplateId {get; set;}
    // public ItemTemplate? ParentItemTemplate {get; set;}
    // public ICollection<ItemTemplate> Children {get; set;} = [];
    // public ItemTemplate(string itemId)
    // {
    //     ItemId = itemId;
    // }

    public ChecklistTemplate? ChecklistTemplate {get; private set;}

    public static ItemTemplate New (string id)
    {
        return new ItemTemplate()
        {
            Id = id
        };
    }

    

    // public ICollection<QuestionTemplate> Questions { get; set; } = [];

    // public void AddQuestionTemplate (QuestionTemplate questionTemplate)
    // {
    //     Questions.Add(questionTemplate);
    // }

    // public ItemTemplate UpdateQuestions(IEnumerable<string>? questions)
    // {
    //     Questions = questions
    //                 ?.Select(q => QuestionTemplate.New(q)) 
    //                 ?.ToList() ?? [];
    //     return this;
    // }
}

