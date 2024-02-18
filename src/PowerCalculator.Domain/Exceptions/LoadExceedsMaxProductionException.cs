namespace PowerCalculator.Domain.Exceptions
{
    public class LoadExceedsMaxProductionException : ProductionPlanBaseException
    {
        public LoadExceedsMaxProductionException() : base("0001", "Insufficient powerplants for the requested load.") { }
    }
}
