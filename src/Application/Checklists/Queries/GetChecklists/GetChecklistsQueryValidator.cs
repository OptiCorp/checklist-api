using Application.Common.Queries;
using FluentValidation;

namespace Application.Checklists.Queries;

public class GetChecklistsQueryValidator : AbstractValidator<GetChecklistsQuery>
{
    public GetChecklistsQueryValidator()
    {
        RuleFor(x => x.MobilizationId)
            .NotEmpty().WithMessage("MobilizationId (Guid) is missing.");
            
        RuleFor(x => x.PageNumber) //TODO:
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}