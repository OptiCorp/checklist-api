using Application.Templates.AddTemplate;
using Application.Templates.UpdateTemplate;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace MobDeMob.Application.Templates.Validators;

public class UpdateTemplateCommandValidator : AbstractValidator<UpdateTemplateCommand>
{
    public UpdateTemplateCommandValidator()
    { 
        RuleFor(v => v.ItemName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(v => v.ItemDescription)
            .MaximumLength(200);

        RuleFor(v => v.Revision)
            .MaximumLength(50);

        RuleFor(v => v.Type)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(v => v.Questions).Must(questions => questions?.Count() <= 20).WithMessage(t => $"The list of questions can not exceed 20, found {t.Questions?.Count()}.");

        RuleForEach(v => v.Questions).NotEmpty().MaximumLength(100).WithMessage("The questions cant be empty strings");

    }
}
