
using Microsoft.Extensions.Logging;
using MobDeMob.Domain.Entities;
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
        //var partTemplates = await SeedPartTemplatesAsync();
        await SeedPartsAsync();
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

    // private async Task<IEnumerable<PartTemplate>?> SeedPartTemplatesAsync()
    // {
    //     if (_modelContextBase.PartTemplates.Any()) return null;

    //     IEnumerable<PartTemplate> partTemplates =
    //     [
    //         new PartTemplate
    //         {
    //             Name = "Skruer",
    //             Type = "Attributes",
    //             Created = DateOnly.FromDateTime(DateTime.Now),
    //             LastModified = DateTime.Now,
    //         },
    //         new PartTemplate
    //         {
    //             Name = "Tapes",
    //             Type = "Attributes",
    //             Created = DateOnly.FromDateTime(DateTime.Now),
    //             LastModified = DateTime.Now,
    //         },
    //         new PartTemplate
    //         {
    //             Name = "Paint",
    //             Type = "Attributes",
    //             Created = DateOnly.FromDateTime(DateTime.Now),
    //             LastModified = DateTime.Now,
    //         }
    //     ];

    //     await _modelContextBase.PartTemplates.AddRangeAsync(
    //         partTemplates
    //     );

    //     return partTemplates;

    // }

    private async Task SeedPartsAsync()
    {
        if (_modelContextBase.Parts.Any() || _modelContextBase.PartTemplates.Any()) return;

        var item = new Item
        {
            Name = "RobotHand",
            PartTemplate = new PartTemplate
            {
                Name = "RobotFingers",
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

        await _modelContextBase.Parts.AddRangeAsync(
            unit
        );
    }
}