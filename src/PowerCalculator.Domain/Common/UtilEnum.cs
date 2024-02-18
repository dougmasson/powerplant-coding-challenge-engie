using System.Runtime.Serialization;

namespace PowerCalculator.Domain.Common
{
    public static class UtilEnum
    {
        /// <summary>
        /// Get value of <see cref="EnumMemberAttribute"/> from  <see cref="Enum"/>.
        /// </summary>
        /// <param name="enum">Value of <see cref="Enum"/>.</param>
        /// <returns>Value of <see cref="EnumMemberAttribute"/>.</returns>
        public static string GetMemberAttrValue(this Enum @enum)
        {
            var attr = @enum.GetType()
                            .GetMember(@enum.ToString()).FirstOrDefault()?
                            .GetCustomAttributes(false).OfType<EnumMemberAttribute>()
                            .FirstOrDefault();

            if (attr is not null)
            {
                return attr.Value!;
            }

            throw new ArgumentException("EnumMemberAttribute not found.", nameof(@enum));
        }

        /// <summary>
        /// Retrieves an <see cref="Enum"/> item from a specified <see cref="string"/> by matching the string with the <see cref="EnumMemberAttribute"/>.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that should be returned.</typeparam>
        /// <param name="value">Value of string will use to find EnumMemberAttribute.</param>
        /// <returns>Value of <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static TEnum GetEnumFromMemberAttrValue<TEnum>(this string? value) where TEnum : Enum
        {
            ArgumentNullException.ThrowIfNull(value);

            foreach (Enum enumItem in Enum.GetValues(typeof(TEnum)))
            {            
                if (string.Equals(enumItem.GetMemberAttrValue(), value, StringComparison.OrdinalIgnoreCase))
                {
                    return (TEnum)enumItem;
                }
            }

            throw new ArgumentException("EnumMemberAttribute not found.", nameof(value));
        }
    }
}
