using Application.Checklists.Queries;
using Application.Mobilizations.Queries.GetMobilizationById;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Checklists.Validators;

public class GetChecklistsForItemBySearchValidator : AbstractValidator<GetChecklistsForItemBySearchQuery>
{
    public GetChecklistsForItemBySearchValidator()
    {
        RuleFor(v => v.itemId).NotEmpty();

        RuleFor(v => v.itemId).MaximumLength(30);

        RuleFor(v => v.checklistSearchId).NotEmpty();

        RuleFor(v => v.checklistSearchId).MaximumLength(50);
    }
}
