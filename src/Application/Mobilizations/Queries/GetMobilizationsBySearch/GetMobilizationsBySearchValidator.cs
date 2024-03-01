using Application.Mobilizations.Queries.GetMobilizationById;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Mobilizations.Queries;

public class GetMobilizationsBySearchValidator : AbstractValidator<GetMobilizationBySearchQuery>
{
    public GetMobilizationsBySearchValidator()
    {
        RuleFor(v => v.title)
            .NotEmpty().WithMessage("cannot search with empty string");
        
        RuleFor(v => v.title)
            .MaximumLength(20);
            
    }
}
