using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MobDeMob.Domain.Common;

namespace MobDeMob.Domain.Entities;
public enum UserStatus
{
    [Display(Name = "Active")]
    Active,
    [Display(Name = "Disabled")]
    Disabled,
    [Display(Name = "Deleted")]
    Deleted,
}
public class User : AuditableEntity
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required string Id { get; set; }

    public required string UmId { get; set; }

    public required string AzureAdUserId { get; set; }

    [MaxLength(150)]
    public required string UserRole { get; set; }

    [MaxLength(50)]
    public required string FirstName { get; set; }

    [MaxLength(50)]
    public required string LastName { get; set; }

    [EmailAddress]
    [MaxLength(100)]
    public required string Email { get; set; }

    [MaxLength(50)]
    public required string Username { get; set; }

    [EnumDataType(typeof(UserStatus))]
    public UserStatus Status { get; set; }

    public DateOnly? LastModified { get; set; }
}

