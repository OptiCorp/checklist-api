using Application.Templates.Commands;
using Application.Upload;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Templates.Validators;


public class AddChecklistTemplateQuestionValidator : AbstractValidator<AddChecklistTemplateQuestionCommand>
{
    public AddChecklistTemplateQuestionValidator()
    {
        RuleFor(v => v.question)
            .MaximumLength(100);

    }
}
