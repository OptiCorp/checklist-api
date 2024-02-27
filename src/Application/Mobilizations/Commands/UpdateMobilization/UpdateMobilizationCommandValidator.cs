using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace MobDeMob.Application.Checklists;

public class UpdateMobilizationCommandValidator : AbstractValidator<UpdateMobilizationCommand>
{
    public UpdateMobilizationCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is requried");
        
        RuleFor(v => v.Title)
            .MaximumLength(50).WithMessage("Title should not exceed 50 characters");

        RuleFor(v => v.Description)
           .MaximumLength(200);
    }
}
