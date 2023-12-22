using System.Diagnostics.CodeAnalysis;

namespace PowerCalculator.Application.Models.Response
{
    [ExcludeFromCodeCoverage]
    public sealed record ErrorResponse
    {
        public int Status { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Type { get; set; }
        public string Instance { get; set; }
        public string ErrorCode { get; set; }
    }
}
