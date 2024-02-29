using Application.Punches.Commands;
using Domain.Entities.ChecklistAggregate;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Checklists.Commands.Validators;

public class SetChecklistQuestionValidator : AbstractValidator<SetChecklistQuestionCheckedCommand>
{
    public SetChecklistQuestionValidator()
    {

    }
}
