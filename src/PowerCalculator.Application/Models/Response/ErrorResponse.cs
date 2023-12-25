using System.Diagnostics.CodeAnalysis;

namespace PowerCalculator.Application.Models.Response
{
    [ExcludeFromCodeCoverage]
    public sealed record ErrorResponse
    {
        public int Status { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
    }
}
