
using Application.Checklists.Commands.AddItem;
using Application.Checklists.Commands.AddPunch;
using FluentValidation;

namespace Application.Checklists.Commands.Validators;

public class AddPunchCommandValidator : AbstractValidator<AddPunchCommand>
{
    public AddPunchCommandValidator()
    {
        RuleFor(v => v.ItemId)
            .NotEmpty()
            .MaximumLength(40);

        RuleFor(v => v.Description)
            .MaximumLength(200);
            
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(50);
    }
}
