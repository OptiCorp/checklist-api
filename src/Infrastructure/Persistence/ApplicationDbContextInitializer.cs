
using Microsoft.Extensions.Logging;

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
        _modelContextBase.Database.EnsureCreated();
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured seeding the database");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        
        await SeedTablesAsync();
        await _modelContextBase.SaveChangesAsync();
    }




    private async Task SeedTablesAsync()

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

        //var item = new Item
        //{
        //    Name = "Robotfinger",
        //    PartTemplate = new PartTemplate
        //    {
        //        Name = "Fingers",
        //        Type = "Attributes",
        //        Created = DateOnly.FromDateTime(DateTime.Now),
        //        LastModified = DateTime.Now,

        //    },
        //    SerialNumber = "dddddd-ldasd",
        //    WpId = "asdddasd",
        //};


        //var subAssembly = new SubAssembly
        //{
        //    Name = "RobotHand",
        //    PartTemplate = new PartTemplate
        //    {
        //        Name = "RobotHands",
        //        Type = "Attributes",
        //        Created = DateOnly.FromDateTime(DateTime.Now),
        //        LastModified = DateTime.Now,
        //    },
        //    SerialNumber = "llllda-ldasd",
        //    WpId = "oodaosd",
        //    Children = [item]
        //};

        //var assembly = new Assembly
        //{
        //    Name = "RobotArm",
        //    PartTemplate = new PartTemplate
        //    {
        //        Name = "RobotArms",
        //        Type = "Attributes",
        //        Created = DateOnly.FromDateTime(DateTime.Now),
        //        LastModified = DateTime.Now,
        //    },
        //    SerialNumber = "ssdknaslk-ldasd",
        //    WpId = "asølkdm",
        //    Children = [subAssembly]
        //};

        //var unit = new Unit
        //{
        //    Name = "Robot",
        //    PartTemplate = new PartTemplate
        //    {
        //        Name = "Skruer",
        //        Type = "Attributes",
        //        Created = DateOnly.FromDateTime(DateTime.Now),
        //        LastModified = DateTime.Now,
        //    },
        //    SerialNumber = "saf2jn1jk2-123",
        //    WpId = "asølkdm",
        //    Children = [assembly],
        //};

        //await _modelContextBase.Parts.AddAsync(
        //    unit
        //);

        //var checklist = new Checklist
        //{
        //    Parts = [item, assembly, subAssembly],
        //};
        //await _modelContextBase.Checklists.AddAsync(checklist);

        //var checklistSec1 = seedChecklistSection(item, ["Does the finger look ok?", "Does the finger have a nail?"], checklist);
        //var checklistSec2 = seedChecklistSection(subAssembly, ["Does the arms look ok?"], checklist);
        //var checklistSec3 = seedChecklistSection(assembly, ["Does the arm look good?"], checklist);
        ////var checklistSec4 = seedChecklistSection(unit, ["Does the robot look ok?"], checklist);

        //checklistSec3.SubSections = [checklistSec2, checklistSec1];

        //await _modelContextBase.ChecklistSections.AddAsync(checklistSec3);

        //await seedMobilization(checklist);
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
