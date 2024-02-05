
using Microsoft.Extensions.Logging;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Entities.Mobilization;
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
        SeedUsersAsync();
        await SeedRestAsync();
        await _modelContextBase.SaveChangesAsync();
    }

    private void SeedUsersAsync()
    {
        if (_modelContextBase.Users.Any()) return;

        _modelContextBase.Users.AddRange(
            new User
            {
                AzureAdUserId = "someid",
                Email = "test@email.com",
                FirstName = "Frank",
                LastName = "Gunvorsen",
                UmId = "Umm",
                Username = "Beast",
                Created = DateOnly.FromDateTime(DateTime.Now),
                CreatedBy = "Robin",
                UserRole = Domain.Enums.UserRole.Admin,
                Status = UserStatus.Active,
                LastModified = DateTime.Now,
            },
        new User
        {
            AzureAdUserId = "someotherid",
            Email = "test2@email.com",
            FirstName = "Kari",
            LastName = "Gunvorsen",
            UmId = "Umm",
            Username = "Beast2",
            Created = DateOnly.FromDateTime(DateTime.Now),
            CreatedBy = "Robin",
            UserRole = Domain.Enums.UserRole.User,
            Status = UserStatus.Active,
            LastModified = DateTime.Now,
        }
        );

    }


    private async Task SeedRestAsync()

    {
        if (_modelContextBase.Parts.Any() 
        || _modelContextBase.PartTemplates.Any() 
        || _modelContextBase.Mobilizations.Any() 
        || _modelContextBase.Checklists.Any() 
        || _modelContextBase.ChecklistSections.Any() 
        || _modelContextBase.ChecklistSectionTemplate.Any()
        
        ) return;

        var item = new Item
        {
            Name = "Robotfinger",
            PartTemplate = new PartTemplate
            {
                Name = "Fingers",
                Type = "Attributes",
                Created = DateOnly.FromDateTime(DateTime.Now),
                LastModified = DateTime.Now,

            },
            SerialNumber = "dddddd-ldasd",
            WpId = "asdddasd",
        };


        var subAssembly = new SubAssembly
        {
            Name = "RobotHand",
            PartTemplate = new PartTemplate
            {
                Name = "RobotHands",
                Type = "Attributes",
                Created = DateOnly.FromDateTime(DateTime.Now),
                LastModified = DateTime.Now,
            },
            SerialNumber = "llllda-ldasd",
            WpId = "oodaosd",
            Children = [item]
        };

        var assembly = new Assembly
        {
            Name = "RobotArm",
            PartTemplate = new PartTemplate
            {
                Name = "RobotArms",
                Type = "Attributes",
                Created = DateOnly.FromDateTime(DateTime.Now),
                LastModified = DateTime.Now,
            },
            SerialNumber = "ssdknaslk-ldasd",
            WpId = "asølkdm",
            Children = [subAssembly]
        };

        var unit = new Unit
        {
            Name = "Robot",
            PartTemplate = new PartTemplate
            {
                Name = "Skruer",
                Type = "Attributes",
                Created = DateOnly.FromDateTime(DateTime.Now),
                LastModified = DateTime.Now,
            },
            SerialNumber = "saf2jn1jk2-123",
            WpId = "asølkdm",
            Children = [assembly],
        };

        await _modelContextBase.Parts.AddAsync(
            unit
        );

        var checklist = new Checklist
        {
            Parts = [unit, item, assembly, subAssembly],
        };
        await _modelContextBase.Checklists.AddAsync(checklist);

        await seedChecklistSection(item, ["Does the finger look ok?", "Does the finger have a nail?"], checklist);
        await seedChecklistSection(unit, ["Does the robot look ok?"], checklist);
        await seedChecklistSection(subAssembly, ["Does the arms look ok?"], checklist);
        await seedChecklistSection(assembly, ["Does the arm look good?"], checklist);

        await seedMobilization(checklist);
    }



    private async Task seedChecklistSection(Part part, List<string> questions, Checklist checklist)
    {
        var checklistSectionTemp = new ChecklistSectionTemplate
        {
            ChecklistQuestion = questions.First(),
        };

        var restQuestions = questions.Skip(1);

        foreach (var q in restQuestions)
        {
            checklistSectionTemp.SubSections.Add(new ChecklistSectionTemplate { ChecklistQuestion = q });
        }

        part.PartTemplate.PartCheckListTemplate = checklistSectionTemp;

        var checklistSection = new ChecklistSection
        {
            Part = part,
            ChecklistSectionTemplate = checklistSectionTemp,
            Checklist = checklist
        };

        await _modelContextBase.ChecklistSections.AddAsync(checklistSection);
    }

    private async Task seedMobilization(Checklist checklist)
    {
        var mob = new Mobilization
        {
            Title = "mobilization to Equinor",
            Description = "some nice description",
            Type = MobilizationType.Mobilization,
            Checklist = checklist
        };

        await _modelContextBase.Mobilizations.AddAsync(mob);
    }
}