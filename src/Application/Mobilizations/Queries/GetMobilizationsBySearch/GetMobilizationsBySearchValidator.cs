using Application.Mobilizations.Queries.GetMobilizationById;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Mobilizations.Queries;

public class GetMobilizationsBySearchValidator : AbstractValidator<GetMobilizationBySearchQuery>
{
    public GetMobilizationsBySearchValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("cannot search with empty string");
        
        RuleFor(v => v.Title)
            .MaximumLength(20);
            
    }
}
