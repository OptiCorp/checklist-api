using Application.Mobilizations.Queries.GetMobilizationById;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Mobilizations.Queries;

public class GetMobilizationsForItemQueryValidator : AbstractValidator<GetChecklistsForItemQuery>
{
    public GetMobilizationsForItemQueryValidator()
    {
        RuleFor(v => v.ItemId)
            .NotEmpty();

        RuleFor(v => v.ItemId)
            .MaximumLength(50);

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1);

    }
}
