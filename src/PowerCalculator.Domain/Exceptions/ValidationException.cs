using PowerCalculator.Domain.Models;

namespace PowerCalculator.Domain.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public List<ErrorDetail> Errors { get; private set; }

        public ValidationException(List<ErrorDetail> errors) : base("One or more validation errors occurred.")
        {
            Errors = errors;
        }
    }
}
