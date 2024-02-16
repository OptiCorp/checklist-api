using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace MobDeMob.Application.Checklists;

public class UpdateMobilizationCommandValidator : AbstractValidator<UpdateMobilizationCommand>
{
    public UpdateMobilizationCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(v => v.Description)
           .MaximumLength(200);
    }
}
