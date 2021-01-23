using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eGym.Application.Services
{
    public interface ITaxCodeService
    {
        Dictionary<string, Regex> CulturePatterns { get; set; }
        Task<bool> ValidateAsync(string taxCode, string culture = "it");
        Task<(DateTime? birthDate, string birthLocation)> GetInfoAsync(string taxCode, string culture = "it");
    }
}
