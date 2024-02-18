namespace PowerCalculator.Domain.Exceptions
{
    public class PowerPlantNotFound : ProductionPlanBaseException
    {
        public PowerPlantNotFound(string name, string type) : base("0002", $"No PowerPlan {name} of type {type}.") { }
    }
}
