using eGym.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eGym.Application.Services
{
    public class TaxCodeService : ITaxCodeService
    {
        private IEnumerable<(int month, string code)> _itaTaxCodeMonths => new (int month, string code)[]
        {
            (month: 1, code: "A"), (month: 2, code: "B"), (month: 3, code: "C"), (month: 4, code: "D"), (month: 5, code: "E"), (month: 6, code: "H"),
            (month: 7, code: "L"), (month: 8, code: "M"), (month: 9, code: "P"), (month: 10, code: "R"), (month: 11, code: "S"), (month: 12, code: "T")
        };

        public TaxCodeService()
        {
            CulturePatterns = new Dictionary<string, Regex>()
            {
                {
                    "it",
                    new Regex(@"([A-Z]{6})(\d{2})([A-Z]{1})(\d{2})([A-Z]{1})(\d{3})([A-Z]{1})", RegexOptions.IgnoreCase)
                },
                {
                    "it-IT",
                    new Regex(@"([A-Z]{6})(\d{2})([A-Z]{1})(\d{2})([A-Z]{1})(\d{3})([A-Z]{1})", RegexOptions.IgnoreCase)
                }
            };
        }

        public Dictionary<string, Regex> CulturePatterns { get; set; }

        public async Task<bool> ValidateAsync(string taxCode, string culture = "it")
            => await Task.FromResult(
                !string.IsNullOrWhiteSpace(taxCode) &&
                CulturePatterns.ContainsKey(culture) &&
                CulturePatterns[culture].IsMatch(taxCode));

        public async Task<(DateTime? birthDate, string birthLocation)> GetInfoAsync(string taxCode, string culture = "it")
        {
            if (string.IsNullOrWhiteSpace(taxCode)) return (null, null);

            if (culture.Equals("it", StringComparison.InvariantCultureIgnoreCase) || culture.Equals("it-IT", StringComparison.InvariantCultureIgnoreCase))
            {
                taxCode = taxCode.ToUpper();
                var tmp = int.Parse(taxCode.Substring(6, 2));
                var year = DateTime.Today.GetCenturyYear() + tmp;
                if (year > DateTime.Today.Year) year = DateTime.Today.AddYears(-100).GetCenturyYear() + tmp;
                var month = _itaTaxCodeMonths.Where(t => t.code == taxCode.Substring(8, 1))?.FirstOrDefault() ?? throw new Exception("Invalid Month");
                var day = int.Parse(taxCode.Substring(9, 2));
                var birthDate = new DateTime(year, month.month, (day > 31 ? day - 40 : day));
                return await Task.FromResult((new DateTime(year, month.month, (day > 31 ? day - 40 : day)), taxCode.Substring(11, 4)));
            }

            return (null, null);
        }
    }
}
