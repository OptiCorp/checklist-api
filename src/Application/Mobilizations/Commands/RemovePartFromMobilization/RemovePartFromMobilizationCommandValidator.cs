using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Mobilizations.Commands;

public class RemovePartFromMobilizationCommandValidator : AbstractValidator<RemoveItemFromMobilizationCommand>
{
    public RemovePartFromMobilizationCommandValidator()
    {
        RuleFor(v => v.ItemId)
            .NotEmpty()
            .MaximumLength(50);
    }
}
