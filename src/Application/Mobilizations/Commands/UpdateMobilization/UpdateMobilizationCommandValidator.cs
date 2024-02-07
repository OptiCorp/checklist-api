using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace MobDeMob.Application.Checklists;

public class UpdateMobilizationCommandValidator : AbstractValidator<UpdateMobilizationCommand>
{
    public UpdateMobilizationCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(v => v.Title)
            .MaximumLength(200);
    }
}