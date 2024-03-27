using FluentValidation.Results;

namespace Application.Mobilizations.Validations;

public class MobilizationValidatorException : Exception
{
    public IEnumerable<ValidationFailure> Failures { get; }

    public MobilizationValidatorException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation failures have occurred.")
    {
        Failures = failures;
    }
}