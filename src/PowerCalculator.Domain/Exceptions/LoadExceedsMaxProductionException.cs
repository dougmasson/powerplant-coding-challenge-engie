namespace PowerCalculator.Domain.Exceptions
{
    public class LoadExceedsMaxProductionException : ProductionPlanBaseException
    {
        public LoadExceedsMaxProductionException() : base("0001", "Insufficient power plants for the requested load.") { }
    }
}
