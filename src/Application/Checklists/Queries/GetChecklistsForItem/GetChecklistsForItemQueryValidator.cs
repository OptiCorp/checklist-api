using Application.Mobilizations.Queries.GetMobilizationById;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Mobilizations.Queries;

public class GetMobilizationsForItemQueryValidator : AbstractValidator<GetChecklistsForItemQuery>
{
    public GetMobilizationsForItemQueryValidator()
    {
        RuleFor(v => v.ItemId)
            .NotEmpty().WithMessage("cannot search with empty string");

        RuleFor(v => v.ItemId)
            .MaximumLength(30).WithMessage("Item id cannot larger than 30 characters");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

    }
}
