using eGym.Common.Security.Criptografy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eGym.Core.SeedWork
{
    public static class SQLDataConverter
    {
        public static string ToSqlString(this IEnumerable<string> source, char separator = ',')
        {
            if (source == null || source.Count() <= 0) return null;
            return string.Join(separator, source);
        }

        public static IEnumerable<string> ToEnumerableStrings(this string source, char separator = ',', StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
        {
            if (string.IsNullOrWhiteSpace(source)) return Array.Empty<string>();
            return source
                .Split(separator, splitOptions)
                ?.Where(t => !string.IsNullOrWhiteSpace(t))
                ?.Select(t => t.Trim());
        }

        //public static IEnumerable<int> ToEnumerableNumber(this string source, char separator = ',', StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
        //{
        //    if (string.IsNullOrWhiteSpace(source)) return Array.Empty<int>();
        //    return source
        //        .Split(separator, splitOptions)
        //        ?.Where(t => !string.IsNullOrWhiteSpace(t) && int.TryParse(t, out int tt))
        //        ?.Select(t => int.Parse(t.Trim()));
        //}
        
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
