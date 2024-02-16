
using Application.Checklists.Commands.AddItem;
using FluentValidation;

namespace Application.Checklists.Commands.Validators;

public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
   public AddItemCommandValidator()
   {
       RuleFor(v => v.ItemId)
        .NotEmpty()
        .MaximumLength(40); 
   }
}
