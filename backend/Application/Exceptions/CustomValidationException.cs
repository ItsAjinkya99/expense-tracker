using FluentValidation.Results;

namespace Application.Exceptions;

public class CustomValidationException:Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public CustomValidationException(IDictionary<string, string[]> errors):base("One or more validation errors occurred.")
    {
        Errors = errors;
    }

    public CustomValidationException(IEnumerable<ValidationFailure> failures) : this(
            failures
            .GroupBy(e => e.PropertyName, e=> e.ErrorMessage)
            .ToDictionary(
                g => g.Key,
                g => g.ToArray()
            ))
    {
    }
}