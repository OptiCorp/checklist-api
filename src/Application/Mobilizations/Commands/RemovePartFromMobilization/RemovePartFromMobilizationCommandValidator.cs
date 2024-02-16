using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Mobilizations.Commands;

public class RemovePartFromMobilizationCommandValidator : AbstractValidator<RemovePartFromMobilizationCommand>
{
    public RemovePartFromMobilizationCommandValidator()
    {
        RuleFor(v => v.PartId)
            .NotEmpty()
            .MaximumLength(40);

    }
}
