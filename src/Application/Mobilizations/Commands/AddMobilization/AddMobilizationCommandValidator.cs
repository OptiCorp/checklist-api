using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Mobilizations.Commands;

public class AddMobilizationCommandValidator : AbstractValidator<AddMobilizationCommand>
{
    public AddMobilizationCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(v => v.Description)
            .MaximumLength(200);

    }
}
