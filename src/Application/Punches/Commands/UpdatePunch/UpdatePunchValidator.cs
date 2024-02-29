using Application.Punches.Commands;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Punches.Validators;

public class UpdatePunchCommandValidator : AbstractValidator<UpdatePunchCommand>
{
    public UpdatePunchCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Cannot set title to emtpty string, spaces or null");

        RuleFor(v => v.Title)
            .MaximumLength(50);

        RuleFor(v => v.Description)
            .MaximumLength(200);
    }
}
