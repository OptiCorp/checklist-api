using Application.Checklists.Queries;
using FluentValidation;

namespace Application.Common.Queries;

public abstract class PaginationQueryValidator<T> : AbstractValidator<PaginationQuery<T>>
{
    protected PaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}