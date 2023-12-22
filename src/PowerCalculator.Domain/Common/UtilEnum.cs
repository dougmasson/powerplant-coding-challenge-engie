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

            if (attr == null)
            {
                return @enum.ToString();
            }

            return attr.Value!;
        }

        /// <summary>
        /// Retrieves an <see cref="Enum"/> item from a specified <see cref="string"/> by matching the string with the <see cref="EnumMemberAttribute"/>.
        /// </summary>
        /// <typeparam name="TEnum">The enum type that should be returned.</typeparam>
        /// <param name="value">Value of string will use to find EnumMemberAttribute.</param>
        /// <returns>Value of <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static TEnum GetEnumFromMemberAttrValue<TEnum>(this string value) where TEnum : Enum
        {
            foreach (Enum enumItem in Enum.GetValues(typeof(TEnum)))
            {
                // Static method not allow to use StringComparer
                if (enumItem.GetMemberAttrValue().Equals(value.ToLower()))
                {
                    return (TEnum)enumItem;
                }
            }

            throw new ArgumentException("Not found.", nameof(value));
        }
    }
}
