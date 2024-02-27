using Application.Punches.Commands;
using Domain.Entities.ChecklistAggregate;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Checklists.Commands.Validators;

public class SetChecklistItemQuestionPatchValidator : AbstractValidator<SetChecklistItemQuestionPatchCheckedCommand>
{
    public SetChecklistItemQuestionPatchValidator()
    {
        RuleFor(v => v.Patches)
            .Must(patches => patches.Operations.Count == 1)
            .WithMessage("Only one patch operation is allowed.");

        RuleFor(v => v.Patches)
            .Must(patches => patches.Operations[0].path.ToLower().TrimStart('/') == nameof(ChecklistItemQuestion.Checked).ToLower())
            .WithMessage("Only the check field is allowed to change are allowed to be changed");
    }
}
