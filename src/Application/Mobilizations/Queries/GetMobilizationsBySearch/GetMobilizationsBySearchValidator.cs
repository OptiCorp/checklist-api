using Application.Mobilizations.Queries.GetMobilizationById;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Mobilizations.Queries;

public class GetMobilizationsBySearchValidator : AbstractValidator<GetMobilizationBySearchQuery>
{
    public GetMobilizationsBySearchValidator()
    {
        // RuleFor(v => v.Title)
        //     .NotEmpty().WithMessage("cannot search with empty string");

        RuleFor(v => v.Title)
            .MaximumLength(50);

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

    }
}
