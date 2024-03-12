using Application.Templates.AddTemplate;
using Application.Templates.UpdateTemplate;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace MobDeMob.Application.Templates.Validators;

public class UpdateTemplateCommandValidator : AbstractValidator<UpdateTemplateCommand>
{
    public UpdateTemplateCommandValidator()
    { 
        // RuleFor(v => v.ItemId)
        //     .NotEmpty().WithMessage("cannot search with empty string");

        // RuleFor(v => v.ItemId)
        //     .MaximumLength(30).WithMessage("Item id cannot larger than 30 characters");

        // RuleFor(v => v.Questions).Must(questions => questions?.Count() <= 20).WithMessage(t => $"The list of questions can not exceed 20, found {t.Questions?.Count()}.");
        RuleFor(v => v.Question)
            .MaximumLength(100).WithMessage("A question can not exceed 100 characters");


        // RuleForEach(v => v.Questions).NotEmpty().MaximumLength(100).WithMessage("The questions cant be empty strings");

    }
}
