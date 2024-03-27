using Application.Templates.GetById;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace MobDeMob.Application.Templates.Validators;

public class GetTemplateQueryValidator : AbstractValidator<GetItemTemplateQuery>
{
    public GetTemplateQueryValidator()
    {
        RuleFor(v => v.ItemTemplateId)
            .NotEmpty();

        RuleFor(v => v.ItemTemplateId)
            .MaximumLength(40);
    }
}
