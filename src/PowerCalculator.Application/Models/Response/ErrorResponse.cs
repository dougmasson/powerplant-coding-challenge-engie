using PowerCalculator.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace PowerCalculator.Application.Models.Response
{
    [ExcludeFromCodeCoverage]
    public sealed record ErrorResponse
    {
        public int Status { get; init; }
        public string Title { get; init; } = string.Empty;
        public string? Detail { get; init; } = null;
        public string Type { get; init; } = string.Empty;
        public string Instance { get; init; } = string.Empty;
        public string? ErrorCode { get; init; } = null;
        public List<ErrorDetail> Erros { get; init; } = null!;
    }
}
