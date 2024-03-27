using Application.Upload;
using FluentValidation;
using MobDeMob.Application.Mobilizations.Commands;

namespace Application.Punches.Validators;

public class PunchUploadFileValidator : AbstractValidator<PunchUploadFile>
{
    public PunchUploadFileValidator()
    {
        RuleFor(v => v.FileName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.ContentType)
           .NotNull()
           .Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png") || x.Equals("application/pdf"))
           .WithMessage("Only JPEG, JPG, PNG, PDF files are allowed");

        //TODO: add validator for the stream?
    }
}


public class PunchUploadFilesCommandValidator : AbstractValidator<PunchUploadFilesCommand>
{
    public PunchUploadFilesCommandValidator()
    {
        RuleFor(v => v.Files)
            .NotEmpty().Must(f => f.Count() <= 5).WithMessage(f => $"A maximum number of 5 files may be uploaded, found: {f.Files.Count()} files.");

        RuleForEach(v => v.Files).SetValidator(new PunchUploadFileValidator());
    }
}
