using Application.Templates.AddTemplate;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace MobDeMob.Application.Templates.Validators;

public class AddTemplateCommandValidator : AbstractValidator<AddTemplateCommand>
{
    public AddTemplateCommandValidator()
    {
        RuleFor(v => v.ItemId)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(v => v.Questions).Must(questions => questions?.Count() <= 20).WithMessage(t => $"The list of questions can not exceed 20, found {t.Questions?.Count()}.");

        RuleForEach(v => v.Questions).NotEmpty().MaximumLength(100).WithMessage("The questions cant be empty strings");

    }
}
