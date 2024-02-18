namespace PowerCalculator.Domain.Exceptions
{
    public class ProductionPlanBaseException : Exception
    {
        public string ErrorCode { get; private set; } = string.Empty;

        public ProductionPlanBaseException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
