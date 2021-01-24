using eGym.Common.Security.Criptografy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eGym.Core.SeedWork
{
    public static class SQLDataConverter
    {
        public static string ToSqlStringFromString(this IEnumerable<string> source, char separator = ',')
        {
            if (source == null || source.Count() <= 0) return null;
            return string.Join(separator, source);
        }

        public static string ToSqlStringFromNumber<T>(this IEnumerable<T> source, char separator = ',')
            where T : IComparable<T>
        {
            if (source == null || source.Count() <= 0) return null;
            return string.Join(separator, source.Select(t => t.ToString()));
        }

        public static string ToSqlStringFromEnum<T>(this IEnumerable<T> source, char separator = ',')
            where T : struct, IConvertible
        {
            if (source == null || source.Count() <= 0) return null;
            return string.Join(separator, source.Select(t => t.ToString()));
        }

        public static IEnumerable<string> ToEnumerableStrings(this string source, char separator = ',', StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
        {
            if (string.IsNullOrWhiteSpace(source)) return Array.Empty<string>();
            return source
                .Split(separator, splitOptions)
                ?.Where(t => !string.IsNullOrWhiteSpace(t))
                ?.Select(t => t.Trim());
        }

        public static IEnumerable<T> ToEnumerableNumber<T>(this string source, char separator = ',', StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
            where T : IComparable<T>
        {
            if (string.IsNullOrWhiteSpace(source)) return Array.Empty<T>();
            return source
                .Split(separator, splitOptions)
                ?.Where(t =>
                {
                    T t1 = (T)Convert.ChangeType(t.Trim(), typeof(T));
                    return !string.IsNullOrWhiteSpace(t) && t1 != null;
                })
                ?.Select(t => (T)Convert.ChangeType(t.Trim(), typeof(T)));
        }

        public static IEnumerable<T> ToEnumerableEnum<T>(this string source, char separator = ',', StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) return null;
            if (string.IsNullOrWhiteSpace(source)) return Array.Empty<T>();
            return source
                .Split(separator, splitOptions)
                ?.Where(t =>
                {
                    T t1 = (T)Enum.Parse(typeof(T), t);
                    return !string.IsNullOrWhiteSpace(t) &&
                        typeof(T).GetFields()
                            .Where(v => v.Name == t1.ToString())
                            .Any();
                })
                ?.Select(t => (T)Enum.Parse(typeof(T), t));
        }

        public static string CryptSensitiveData(this string value, string token = null)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            var twoway = new TwoWay();
            return twoway.Crypt(text: value, customToken: token);
        }

        public static string DecryptSensitiveData(this string value, string token = null)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            var twoway = new TwoWay();
            return twoway.Decrypt(text: value, customToken: token);
        }
    }

}
