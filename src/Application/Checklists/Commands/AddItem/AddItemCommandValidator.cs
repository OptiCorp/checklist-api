
using Application.Checklists.Commands.AddItem;
using FluentValidation;

namespace Application.Checklists.Commands.Validators;

public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
   public AddItemCommandValidator()
   {
      RuleFor(v => v.ItemId)
         .NotEmpty().WithMessage("cannot search with empty string");

      RuleFor(v => v.ItemId)
          .MaximumLength(30).WithMessage("Item id cannot larger than 30 characters");
   }
}
