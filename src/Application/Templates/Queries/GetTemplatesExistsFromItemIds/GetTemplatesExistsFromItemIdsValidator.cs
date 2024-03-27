using Application.Templates.Queries;
using Application.Upload;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Templates.Validators;

public class GetTemplatesExistsFromItemIdsValidator : AbstractValidator<GetTemplatesExistsFromItemIdsQuery>
{
    public GetTemplatesExistsFromItemIdsValidator()
    {
        RuleForEach(i => i.ItemIds).NotEmpty();

        RuleForEach(i => i.ItemIds).MaximumLength(30);
    }
}


