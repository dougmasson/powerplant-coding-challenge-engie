namespace PowerCalculator.Domain.Exceptions
{
    public class ProductionPlanBaseException : Exception
    {
        public string ErrorCode { get; private set; } = string.Empty;

        public ProductionPlanBaseException() { }

        public ProductionPlanBaseException(string message) : base(message) { }

        public ProductionPlanBaseException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
