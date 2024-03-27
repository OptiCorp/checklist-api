﻿
using Domain.Entities;
using Domain.Entities.ChecklistAggregate;
using Domain.Entities.TemplateAggregate;
using Microsoft.Extensions.Logging;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Enums;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Infrastructure;

//NOTE: very similar to: https://github.com/jasontaylordev/CleanArchitecture/blob/net7.0/src/Infrastructure/Persistence/ApplicationDbContextInitialiser.cs
public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ModelContextBase _modelContextBase;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ModelContextBase modelContextBase)
    {
        _logger = logger;
        _modelContextBase = modelContextBase;
        //_modelContextBase.Database.EnsureCreated();
    }

    public async Task SeedAsync()
    {

        TrySeed();


    }

    private void TrySeed()
    {
        SeedTables();
        _modelContextBase.SaveChanges();
    }




    private void SeedTables()

    {
        if (_modelContextBase.Items.Any()
        || _modelContextBase.ItemTemplates.Any()
        || _modelContextBase.Mobilizations.Any()
        || _modelContextBase.Checklists.Any()
        || _modelContextBase.ChecklistCollections.Any()
        || _modelContextBase.ChecklistQuestions.Any()
        || _modelContextBase.Punches.Any()
        || _modelContextBase.QuestionTemplates.Any()
        ) return;

        for (var i = 0; i < 100; i++)
        {
            

            var checklistCollection = CreateChecklistCollection();
            CreateMobilization($"{i}Test title{i}", MobilizationType.Mobilization, MobilizationStatus.NotReady, checklistCollection.Id, "Cool description");
            var questionTemplates = CreateQuestionTemplates();

            var itemTemplate = CreateItemTemplate();

            var item = CreateItem(itemTemplate.Id);

            var checklistTemplate = CreateChecklistTemplate(itemTemplate.Id, questionTemplates);

            var checklist = CreateChecklist(checklistCollection.Id, item.Id, checklistTemplate.Id);

            foreach (var qt in questionTemplates)
            {
                var check = i % 2 == 0;

                CreateChecklistQuestion(checklist.Id, qt, check, !check);
            }
        }
    }

    private string CreateRandomString()
    {
        Random random = new Random();
        int length = 10;
        string randomString = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return randomString;
    }

    private Item CreateItem(string itemTemplateId)
    {
        var id = CreateRandomString();
        var item = Item.New(itemTemplateId, id);

        _modelContextBase.Items.Add(item);
        return item;
    }

    private ItemTemplate CreateItemTemplate()
    {
        var id = CreateRandomString();
        var itemTemplate = ItemTemplate.New(id);

        _modelContextBase.ItemTemplates.Add(itemTemplate);
        return itemTemplate;
    }

    private ChecklistTemplate CreateChecklistTemplate(string itemTemplateId, ICollection<QuestionTemplate> questions)
    {
        var checklistTemplate = ChecklistTemplate.New(itemTemplateId, questions); 

        _modelContextBase.ChecklistTemplates.Add(checklistTemplate); //TODO:
        return checklistTemplate;
    }

    private ICollection<QuestionTemplate> CreateQuestionTemplates()
    {
        ICollection<QuestionTemplate> questionTemplates = [
            QuestionTemplate.New("Is the bolt ok?"),
            QuestionTemplate.New("Does it work?"),
            QuestionTemplate.New("Is the bolt very pretty?"),
        ];
        return questionTemplates;
    }

    private Checklist CreateChecklist(Guid checklistCollectionId, string itemId, Guid checklistTemplateId)
    {
        var checklist = Checklist.New(itemId, checklistCollectionId, checklistTemplateId);
        checklist.SetChecklistStatus(ChecklistStatus.NotStarted);
        _modelContextBase.Checklists.Add(checklist);
        return checklist;
    }

    private ChecklistQuestion CreateChecklistQuestion(Guid checklistId, QuestionTemplate template, bool isChecked, bool isNA)
    {
        var checklistQuestion = new ChecklistQuestion(template, checklistId);
        checklistQuestion.MarkQuestionAsCheckedOrUnChecked(isChecked);
        checklistQuestion.MarkQuestionAsNotApplicable(isNA);

        _modelContextBase.ChecklistQuestions.Add(checklistQuestion);
        return checklistQuestion;
    }

    private Mobilization CreateMobilization(string title, MobilizationType mobilizationType, MobilizationStatus status, Guid checklistCollectionId, string? description = "")
    {
        var mob = Mobilization.New(title, mobilizationType, status, checklistCollectionId, description);
        _modelContextBase.Mobilizations.Add(mob);
        return mob;
    }

    private ChecklistCollection CreateChecklistCollection()
    {
        var cc = new ChecklistCollection();
        _modelContextBase.ChecklistCollections.Add(cc);
        return cc;
    }

    //private static ChecklistSection seedChecklistSection(Part part, List<string> questions, Checklist checklist)
    //{
    //    var checklistSectionTemp = new ChecklistSectionTemplate
    //    {
    //        ChecklistQuestion = questions.First(),
    //    };

    //    var restQuestions = questions.Skip(1);

    //    foreach (var q in restQuestions)
    //    {
    //        checklistSectionTemp.SubSections.Add(new ChecklistSectionTemplate { ChecklistQuestion = q });
    //    }

    //    part.PartTemplate.PartCheckListTemplate = checklistSectionTemp;

    //    var checklistSection = new ChecklistSection
    //    {
    //        Part = part,
    //        ChecklistSectionTemplate = checklistSectionTemp,
    //        Checklist = checklist,

    //    };

    //    return checklistSection;
    //}

    //private async Task seedMobilization(Checklist checklist)
    //{
    //    var mob = new Mobilization
    //    {
    //        Title = "mobilization to Equinor",
    //        Description = "some nice description",
    //        Type = MobilizationType.Mobilization,
    //        ChecklistId = checklist.Id,
    //        Checklist = checklist
    //    };

    //    await _modelContextBase.Mobilizations.AddAsync(mob);
    //}
}
