using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;
using MobDeMob.Application.Punches.Commands;

namespace MobDeMob.Application.Checklists;

public class CreatePunchCommandValidator : AbstractValidator<CreatePunchCommand>
{
    public CreatePunchCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(50)
            .NotEmpty();
        
        RuleFor(v => v.Description)
            .MaximumLength(200)
            .NotEmpty();
    }
}