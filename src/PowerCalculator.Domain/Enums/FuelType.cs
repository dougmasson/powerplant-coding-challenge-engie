using System.Runtime.Serialization;

namespace PowerCalculator.Domain.Enums
{
    public enum FuelType
    {
        [EnumMember(Value = "gasfired")]
        Gas = 0,

        [EnumMember(Value = "turbojet")]
        Kerosine = 1,

        [EnumMember(Value = "windturbine")]
        Wind = 2
    }
}
