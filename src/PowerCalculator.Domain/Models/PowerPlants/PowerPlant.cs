using PowerCalculator.Domain.Enums;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PowerCalculator.Domain.Models
{
    [ExcludeFromCodeCoverage]
    [DebuggerDisplay("Name: {Name} | Type: {Type} | CanOperate: {CanOperate} | PowerCapacityToGenerate: " +
                     "{PowerCapacityToGenerate.ToString(\"N2\")} | PowerToGenerateForPlan: {PowerToGenerateForPlan.ToString(\"N2\")}")]
    /// <summary>
    /// PowerPlant base.
    /// </summary>
    public abstract class PowerPlant
    {
        /// <summary>
        /// Name of plant.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of fuel.
        /// </summary>
        public abstract FuelType Type { get; }

        /// <summary>
        /// Efficiency.
        /// </summary>
        public double Efficiency { get; set; }

        /// <summary>
        /// Minimum Power.
        /// </summary>
        public double PowerMinimum { get; set; }

        /// <summary>
        /// Maximum.
        /// </summary>
        public double PowerMaximum { get; set; }

        /// <summary>
        /// Cost of Power.
        /// </summary>
        public double PowerCost { get; set; }

        /// <summary>
        /// Power capacity to generate.
        /// </summary>
        public double PowerCapacityToGenerate { get; set; }

        /// <summary>
        /// Generated power for plan.
        /// </summary>
        public double PowerGeneratedForPlan { get; set; }

        /// <summary>
        /// Previous value of <see cref="PowerGeneratedForPlan"/>.
        /// </summary>
        public double PowerGeneratedForPlanOld { get; set; }

        /// <summary>
        /// Flag to identify if plant is be able to operate.
        /// </summary>
        /// <remarks>
        /// Is <c>True</c> when:
        /// <para>- Exists power capacity to generate for plan.</para>
        /// <para>- Minimum power for amount of energy (MWh) that needs to be generated for plan.</para>
        /// </remarks>
        public bool CanOperate { get; set; }

        public abstract void CalculatePowerCost(FuelsInfo fuelInfo);
        public abstract void CalculatePowerCapacityToGenerate(FuelsInfo fuelInfo);

        /// <summary>
        /// Calculate <see cref="PowerCost"/> and <see cref="PowerCapacityToGenerate"/>.
        /// </summary>
        /// <param name="fuelInfo">Value of <see cref="FuelsInfo"/>.</param>
        public void CalculatePower(FuelsInfo fuelInfo)
        {
            CalculatePowerCost(fuelInfo);
            CalculatePowerCapacityToGenerate(fuelInfo);
        }
    }
}
