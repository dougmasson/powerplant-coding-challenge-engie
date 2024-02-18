using System.Diagnostics.CodeAnalysis;

namespace PowerCalculator.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public sealed record ErrorDetail(string PropertyName, string ErrorMessage) { }
}