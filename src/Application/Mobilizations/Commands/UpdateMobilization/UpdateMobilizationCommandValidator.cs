using Application.Mobilizations.Validations;
using FluentValidation;
using FluentValidation.Results;
using MobDeMob.Application.Mobilizations.Commands;

namespace MobDeMob.Application.Checklists;

public class UpdateMobilizationCommandValidator : AbstractValidator<UpdateMobilizationCommand>
{
    public UpdateMobilizationCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty();

        RuleFor(v => v.Title)
            .MaximumLength(50);

        RuleFor(v => v.Description)
           .MaximumLength(200);
    }
}
