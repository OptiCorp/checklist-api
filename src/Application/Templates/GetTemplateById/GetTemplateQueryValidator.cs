using Application.Templates.AddTemplate;
using Application.Templates.GetById;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace MobDeMob.Application.Templates.Validators;

public class GetTemplateQueryValidator : AbstractValidator<GetTemplateQuery>
{
    public GetTemplateQueryValidator()
    {
        RuleFor(v => v.ItemId)
            .NotEmpty().WithMessage("cannot search with empty string");

        RuleFor(v => v.ItemId)
            .MaximumLength(30).WithMessage("Item id cannot larger than 30 characters");
    }
}
