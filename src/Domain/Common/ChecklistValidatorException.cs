
namespace Domain.Common.Exceptions;

public class ChecklistValidationException : Exception
{
    public ChecklistValidationException(string message)
        : base(message)
    {

    }

    // public ChecklistValidationException(IEnumerable<ValidationFailure> failures)
    //     : this()
    // {
    //     Errors = failures
    //         .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
    //         .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    // }

}