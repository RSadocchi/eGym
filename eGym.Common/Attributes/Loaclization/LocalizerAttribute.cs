using System;
using System.Collections.Generic;
using System.Linq;

namespace eGym.Common.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class LocalizerAttribute : Attribute
    {
        public string Value { get; set; } = null;
        public string CultureCode { get; set; } = "it";
        public object EnumValue { get; set; } = null;

        public LocalizerAttribute(string value, string culture)
        {
            Value = value;
            CultureCode = culture;
        }

        public LocalizerAttribute(string value, string culture, object enumValue)
        {
            Value = value;
            CultureCode = culture;
            EnumValue = enumValue;
        }
    }

    public static partial class Extensions
    {
        public static string GetLocalizedValue(this Type source, System.Globalization.CultureInfo culture)
        {
            var values = source.GetLocalizedValues()
                .Where(t => new System.Globalization.CultureInfo(t.CultureCode) == culture)
                .ToList();
            return values.Select(o => o.Value).FirstOrDefault();
        }

        public static IEnumerable<LocalizerAttribute> GetLocalizedValues(this Type source)
            => source
                .GetFields()
                .Where(m => m.GetCustomAttributes(typeof(LocalizerAttribute), false).Count() > 0)
                .SelectMany(m => m.GetLocalizedValues());

        public static IEnumerable<IEnumerable<LocalizerAttribute>> GetLocalizedPairedValues(this Type source)
            => source
                .GetFields()
                .Where(m => m.GetCustomAttributes(typeof(LocalizerAttribute), false).Count() > 0)
                .Select(m => m.GetLocalizedValues());

        public static IEnumerable<LocalizerAttribute> GetLocalizedValues(this System.Reflection.FieldInfo info)
            => ((LocalizerAttribute[])info.GetCustomAttributes(typeof(LocalizerAttribute), false));

        public static TValue GetMemberFromLocalizedValue<TSource, TValue>(this Type source, string localizedValue, string culture)
            where TSource : struct, IConvertible
        {
            if (!typeof(TSource).IsEnum) throw new ArgumentException($"{nameof(source)} is not Enum type.");
            return (TValue)
                source.GetLocalizedValues()
                    .Where(t => t.CultureCode.ToLower() == culture.ToLower() && t.Value?.ToLower() == localizedValue?.ToLower())
                    .Select(t => t.EnumValue)
                    .FirstOrDefault();
        }
    }
}
