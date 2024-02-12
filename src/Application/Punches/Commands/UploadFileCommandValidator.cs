using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;
using MobDeMob.Application.Punches.Commands;

namespace MobDeMob.Application.Checklists;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(v => v.Stream)
            .NotNull();
            
    }
}