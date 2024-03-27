
using Application.Checklists.Commands.AddItem;
using Application.Checklists.Commands.AddPunch;
using FluentValidation;

namespace Application.Checklists.Commands.Validators;

public class AddPunchCommandValidator : AbstractValidator<AddPunchCommand>
{
    public AddPunchCommandValidator()
    {
        RuleFor(v => v.itemId)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(v => v.description)
            .MaximumLength(200);
            
        RuleFor(v => v.title)
            .NotEmpty()
            .MaximumLength(50);
    }
}
