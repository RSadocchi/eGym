using eGym.Application.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eGym.Application.Services
{
    public interface IComuniItalianiService
    {
        Task<List<ComuneItalianoJson>> GetJsonDataAsync();
        Task<List<ComuneItalianoCsv>> GetCsvDataAsync();

        Task<List<ComuneItalianoJson>> GetFromJsonByAsync(
            string CAP = null,
            string codiceCatastale = null);
        Task<List<ComuneItalianoCsv>> GetFromCsvByAsync(
            string nazione = null,
            string comune = null,
            string comuneInizio = null,
            string comuneContiene = null,
            string comuneInt = null,
            string comuneIntInizio = null,
            string comuneIntContiene = null,
            string codiceCatastale = null,
            bool? archiviato = null);
    }

}
