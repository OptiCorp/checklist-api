using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace MobDeMob.Application.Checklists.Commands;

public class DeleteChecklistCommandValidator : AbstractValidator<DeleteChecklistCommand>
{
    public DeleteChecklistCommandValidator()
    {

    }
}
