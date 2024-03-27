using Application.Templates.AddTemplate;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace MobDeMob.Application.Templates.Validators;

public class AddTemplateCommandValidator : AbstractValidator<AddChecklistTemplateCommand>
{
    public AddTemplateCommandValidator()
    {
        RuleFor(v => v.itemTemplateId)
            .NotEmpty();

        RuleFor(v => v.itemTemplateId)
            .MaximumLength(30);

        RuleFor(v => v.Questions).Must(questions => questions?.Count() <= 20).WithMessage(t => $"The list of questions can not exceed 20, found {t.Questions?.Count()}.");

        RuleForEach(v => v.Questions).NotEmpty();

        RuleForEach(v => v.Questions).MaximumLength(100);

    }
}
