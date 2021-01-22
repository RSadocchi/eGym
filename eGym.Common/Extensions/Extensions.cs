using eGym.Common.InternalServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eGym.Common
{
    public enum MonthCodesEnum { A = 1, B, C, D, E, H, L, M, P, R, S, T }

    public enum DefaultRegexEnum
    {
        TaxCode_ITA,
        VATNo_ITA,
        GenericDate_DMY,
        GenericDate_YMD
    }
    
    public static class Extensions
    {
        public static int GetCenturyYear(this DateTime dateTime)
            => (int)(dateTime.Year / 100) * 100;

        public static long ToUnixEpochDate(this DateTime dateTime)
            => (long)Math.Round((dateTime.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public static Dictionary<DefaultRegexEnum, string> DefaultRegex
        {
            get
            {
                return new Dictionary<DefaultRegexEnum, string>
                {
                    { DefaultRegexEnum.TaxCode_ITA, @"([A-Z]{6})(\d{2})([A-Z]{1})(\d{2})([A-Z]{1})(\d{3})([A-Z]{1})" },
                    { DefaultRegexEnum.VATNo_ITA, @"((IT)?(\d{11}))" },
                    { DefaultRegexEnum.GenericDate_DMY, @"(\d{1,2}[\/\-\.]{1}\d{1,2}[\/\-\.]{1}\d{2,4})\b" },
                    { DefaultRegexEnum.GenericDate_YMD, @"(\d{2,4}[\/\-\.]{1}\d{1,2}[\/\-\.]{1}\d{1,2})\b" }
                };
            }
        }

        public static IEnumerable<string> SpecialChars => new string[] { "£", "$", "&", "@", ".", "-", "_", "!", "=", "à", "é", "è", "ì", "ò", "ù" };

        public static bool ContainNumbers(this string source) => Regex.IsMatch(source, @"\d");

        public static bool ContainsUpperCase(this string source) => Regex.IsMatch(source, @"[A-Z]");

        public static bool ContainsSpecialChar(this string source, string pattern = null) => Regex.IsMatch(source, (pattern ?? $"[{string.Join("", SpecialChars)}]"));

        public static string ToFormattedMoneyString(this decimal val, CultureInfo culture = null)
        {
            var rtn = string.Empty;
            if (culture == null) culture = CultureInfo.CurrentUICulture;
            rtn = string.Format(new Formatters(culture), "{0:C2}", val);
            return rtn;
        }

        public static bool IsValidEmailAddress(this string email) => (new Validators()).MailAddress(email);

        public static string ToPlainText(this string source, string pattern = @"<[^>]*>", string replace = "") => Regex.Replace(source, pattern, replace);

        #region Numbers
        public static bool IsEven(this int num) => (num % 2 == 0);
        public static bool IsEven(this long num) => (num % 2 == 0);
        public static bool IsEven(this double num) => (num % 2 == 0);
        public static bool IsEven(this float num) => (num % 2 == 0);
        public static bool IsEven(this short num) => (num % 2 == 0);
        public static bool IsEven(this decimal num) => (num % 2 == 0);
        #endregion

        public static string GeneratePassword(int length = 8)
        {
            Random rnd = new Random();
            string password = string.Empty;
            while (password.Length < length)
            {
                int c = rnd.Next(33, 123);
                if ((c >= 48 && c <= 57) || /* 0-9 */
                    (c >= 65 && c <= 90) || /* A-Z */
                    (c >= 97 && c <= 122) || /* a-z */
                    c == 33 || (c >= 36 && c <= 38) || c == 95 /* ! # $ % & _ */)
                    password += (char)c;
            }
            return password;
        }

        public static string ToMD5Hash(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return null;
            var sb = new StringBuilder();
            var csp = new MD5CryptoServiceProvider();
            var bytes = csp.ComputeHash(Encoding.UTF8.GetBytes(source));
            bytes.ToList().ForEach(b => { sb.Append(b.ToString("x2")); });
            return sb.ToString();
        }

        #region String to Byte array | base64 and Back
        public static string ToBase64(this string source, Encoding encoding = null)
        {
            var rtn = Convert.ToBase64String(source.ToByteArray(encoding));
            return rtn;
        }

        public static string BackFromBase64(this string source, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            var rtn = encoding.GetString(Convert.FromBase64String(source));
            return rtn;
        }

        public static byte[] ToByteArray(this Stream stream)
        {
            byte[] output = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(output, 0, output.Length)) > 0)
                    ms.Write(output, 0, read);
                return ms.ToArray();
            }
        }

        public static byte[] ToByteArray(this string source, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            return encoding.GetBytes(source);
        }

        public static byte[] GetFileContentByteArray(this string path, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            return System.IO.File.ReadAllText(path, encoding).ToByteArray();
        }

        public static byte[] ToByteArrayFromBase64(this string base64) => Convert.FromBase64String(base64);

        public static Stream ToStream(this string source, Encoding encoding = null)
        {
            byte[] byteArray = source.ToByteArray(encoding);
            return new MemoryStream(byteArray);
        }

        public static async Task<string> ToStringBackAsync(this byte[] source)
        {
            string text = null;
            using (var ms = new MemoryStream(source))
            using (var sr = new StreamReader(ms))
                text = await sr.ReadToEndAsync();
            return await Task.FromResult(text);
        }

        public static async Task<string> ToStringBackAsync(this Stream source)
        {
            string text = null;
            using (var sr = new StreamReader(source))
                text = await sr.ReadToEndAsync();
            return await Task.FromResult(text);
        }
        #endregion

        public static string ToCapitalize(this string word)
            => $"{word.Trim().ToUpper().Substring(0, 1)}{(word.Length > 1 ? word.Trim().ToLower().Substring(1) : string.Empty)}";

        public static IEnumerable<string> ToCapitalize(this string[] words)
        {
            foreach (var word in words)
                yield return word.ToCapitalize();
        }

        public static int LevenshteinDistance(this string source, string compare, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            int n = source.Length;
            int m = compare.Length;
            int[,] d = new int[n + 1, m + 1];
            if (n == 0) return m;
            if (m == 0) return n;
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    int cost = compare[j - 1].ToString().Equals(source[i - 1].ToString(), comparison) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            return d[n, m];
        }

        public static bool LevenshteinDistanceInString(this string source, IEnumerable<string> compares, int maxDistance, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            var match = false;
            foreach (var compare in compares.Where(t => t.Length > maxDistance))
                if (source.Length > (maxDistance + 1)
                    && maxDistance >= LevenshteinDistance(source, compare, comparison)
                    && (source.Length == compare.Length || Math.Abs(source.Length - compare.Length) == maxDistance)
                    && ((source.Length - compare.Length) == 0 && source.Substring(0, source.Length - maxDistance).StartsWith(compare.Substring(0, compare.Length - maxDistance), comparison) ? true :
                        (source.Length - compare.Length) < 0 ?
                            compare.Substring(0, source.Length).Equals(source, comparison) :
                            source.Substring(0, compare.Length).Equals(compare, comparison)
                    ))
                    match = true;
            return match;
        }

        public static bool StartsWith(this IEnumerable<string> words, string comparer, StringComparison comparison)
        {
            if (string.IsNullOrEmpty(comparer)) return false;
            foreach (var word in words)
                if (comparer.Trim().StartsWith(word.Trim(), comparison)) return true;
            return false;
        }

        public static bool HasMatch(this IEnumerable<string> words, string comparer)
        {
            if (string.IsNullOrEmpty(comparer)) return false;
            foreach (var word in words)
                if (comparer.Trim().ToLower().StartsWith(word.Trim().ToLower()) || comparer.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(comparer.ToLower())) return true;
            return false;
        }

        public static bool HasMatch(this IEnumerable<string> words, string comparer, StringComparison comparison)
        {
            if (string.IsNullOrEmpty(comparer)) return false;
            foreach (var word in words)
                if (comparer.Trim().StartsWith(word.Trim(), comparison) || comparer.ToLower().Contains(word.ToLower()) || word.ToLower().Contains(comparer.ToLower())) return true;
            return false;
        }

        public static string RemoveSpecialCharacters(this string @string, bool removeSpaces = true, params char[] allowed)
        {
            StringBuilder sb = new StringBuilder();
            if (removeSpaces) @string = @string.Replace("&nbsp;", "");
            if (!removeSpaces)
            {
                if (allowed == null) allowed = (new List<char>()).ToArray();
                if (!allowed.Contains(' '))
                {
                    var tmp = allowed.ToList();
                    tmp.Add(' ');
                    allowed = tmp.ToArray();
                }
            }
            foreach (char c in @string)
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || allowed.Contains(c)) sb.Append(c);
            return Regex.Replace(sb.ToString(), @"\s+", " ").Trim();
        }

        public static string ToUrlFriendlyName(this string source)
            => source
                .Trim()
                .ToLower()
                .Replace(" ", "-")
                .Replace('é', 'e').Replace('è', 'e')
                .Replace('à', 'a')
                .Replace('ì', 'i')
                .Replace('ò', 'o')
                .Replace('ù', 'u')
                .Replace("'", "")
                .Replace("/", "-")
                .Replace(@"\", "-")
                .Replace("|", "-")
                .RemoveSpecialCharacters(allowed: '-');

        public static decimal FloorTo(this decimal value, int digit = 1)
        {
            value /= digit;
            value = Math.Floor(value);
            return value * digit;
        }
        public static decimal CeilingTo(this decimal value, int digit = 1)
        {
            value /= digit;
            value = Math.Ceiling(value);
            return value * digit;
        }

        public static bool IsValidTaxCode(this string source, string culture = "it")
        {
            culture = culture?.ToLower()?.Trim();
            switch (culture)
            {
                case "it":
                case "it-it":
                    return Regex.IsMatch(source, DefaultRegex[DefaultRegexEnum.TaxCode_ITA], RegexOptions.IgnoreCase);
                default:
                    return false;
            }
        }

        public static DateTime? TaxCodeToDateOfBirth(this string tax, string culture = "it")
        {
            culture = culture?.ToLower().Trim();
            if (!tax.IsValidTaxCode(culture)) return null;
            switch (culture)
            {
                case "it":
                case "it-it":
                    var yy = _getYear(int.Parse(tax.Substring(6, 2)));
                    var mm = (int)Enum.Parse(typeof(MonthCodesEnum), tax.Substring(8, 1).ToUpper(), true);
                    var dd = _getDay(int.Parse(tax.Substring(9, 2)));
                    return new DateTime(yy, mm, dd, 0, 0, 0);
                default:
                    return null;
            }

            int _getYear(int yy)
            {
                var year = DateTime.Today.Year;
                var delta = (int)((decimal)year).FloorTo(1000);
                return
                    (year % delta) >= yy ?
                    yy + delta :
                    yy + (delta - 100);
            }
            int _getDay(int dd) => dd > 31 ? dd - 40 : dd;
        }

        public static string PhoneNumberNomalize(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return null;
            var normalized = source.Trim();
            if (normalized.StartsWith("+")) normalized = "00" + normalized.Substring(1);
            normalized = Regex.Replace(normalized, @"[\s\W]", "");
            return normalized;
        }
    }
}
