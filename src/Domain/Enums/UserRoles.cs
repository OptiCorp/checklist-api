using System.ComponentModel.DataAnnotations;
namespace MobDeMob.Domain.Enums;
public enum UserRole
{
    [Display(Name = "User")]
    User,
    [Display(Name = "Admin")]
    Admin,
}