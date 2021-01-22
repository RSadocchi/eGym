using CsvHelper;
using eGym.Application.Model;
using eGym.Application.Option;
using eGym.Core.SeedWork.NSpecifications;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.Application.Services
{
    public class ComuniItalianiService : IComuniItalianiService
    {
        readonly ApplicationResources _appResources;

        private List<ComuneItalianoJson> ListaComuniJson { get; set; }
        private List<ComuneItalianoCsv> ListaComuniCsv { get; set; }

        public ComuniItalianiService(
            IOptions<ApplicationResources> appResources)
        {
            _appResources = appResources.Value ?? throw new ArgumentNullException(nameof(appResources));
            GetJsonDataAsync().Wait();
            GetCsvDataAsync().Wait();
        }


        public async Task<List<ComuneItalianoJson>> GetJsonDataAsync()
        {
            if (ListaComuniJson == null || ListaComuniJson?.Count() <= 0)
            {
                string json = File.ReadAllText(_appResources.ComuniItalianiJson);
                ListaComuniJson = JsonConvert.DeserializeObject<List<ComuneItalianoJson>>(json);
            }
            return await Task.FromResult(this.ListaComuniJson);
        }

        public async Task<List<ComuneItalianoCsv>> GetCsvDataAsync()
        {
            if (ListaComuniCsv == null || ListaComuniCsv?.Count() <= 0)
            {
                using (var reader = new StreamReader(_appResources.ComuniItalianiCsv))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    //csv.Configuration.HasHeaderRecord = true;
                    //csv.Configuration.Delimiter = ";";
                    ListaComuniCsv = csv.GetRecords<ComuneItalianoCsv>().ToList();
                }
            }
            return await Task.FromResult(this.ListaComuniCsv);
        }


        public async Task<List<ComuneItalianoJson>> GetFromJsonByAsync(
            string CAP = null,
            string codiceCatastale = null)
        {
            var spec = Spec.Any<ComuneItalianoJson>();
            if (!string.IsNullOrWhiteSpace(CAP))
            {
                CAP = CAP.Replace(" ", "");
                spec &= new Spec<ComuneItalianoJson>(t => t.CAP.Any(i => i.ToUpper() == CAP.ToUpper()));
            }
            if (!string.IsNullOrWhiteSpace(codiceCatastale))
            {
                codiceCatastale = codiceCatastale.Replace(" ", "");
                spec &= new Spec<ComuneItalianoJson>(t => t.CodiceCatastale.ToUpper() == codiceCatastale.ToUpper());
            }
            return await Task.FromResult(ListaComuniJson.AsQueryable().Where(spec.Expression).ToList());
        }

        public async Task<List<ComuneItalianoCsv>> GetFromCsvByAsync(
            string nazione = null,
            string comune = null,
            string comuneInizio = null,
            string comuneContiene = null,
            string comuneInt = null,
            string comuneIntInizio = null,
            string comuneIntContiene = null,
            string codiceCatastale = null,
            bool? archiviato = null)
        {
            var spec = Spec.Any<ComuneItalianoCsv>();
            if (!string.IsNullOrWhiteSpace(nazione))
            {
                nazione = nazione.Replace(" ", "");
                spec &= new Spec<ComuneItalianoCsv>(t => t.NAZIONE.ToUpper() == nazione.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(comune) || !string.IsNullOrWhiteSpace(comuneInt))
            {
                comune = comune.Replace(" ", "").ToUpper();
                comuneInt = comuneInt.Replace(" ", "").ToUpper();
                spec &= new Spec<ComuneItalianoCsv>(t => t.COMUNE.ToUpper().Equals(comune) || t.COMUNE_INT.ToUpper().Equals(comuneInt));
            }
            if (!string.IsNullOrWhiteSpace(comuneInizio) || !string.IsNullOrWhiteSpace(comuneIntInizio))
            {
                comuneInizio = comuneInizio.Replace(" ", "").ToUpper();
                comuneIntInizio = comuneIntInizio.Replace(" ", "").ToUpper();
                spec &= new Spec<ComuneItalianoCsv>(t => t.COMUNE.ToUpper().StartsWith(comuneInizio) || t.COMUNE_INT.ToUpper().StartsWith(comuneIntInizio));
            }
            if (!string.IsNullOrWhiteSpace(comuneContiene) || !string.IsNullOrWhiteSpace(comuneIntContiene))
            {
                comuneContiene = comuneContiene.Replace(" ", "").ToUpper();
                comuneIntContiene = comuneIntContiene.Replace(" ", "").ToUpper();
                spec &= new Spec<ComuneItalianoCsv>(t => t.COMUNE.ToUpper().Contains(comuneContiene) || t.COMUNE_INT.ToUpper().Contains(comuneIntContiene));
            }
            if (!string.IsNullOrWhiteSpace(codiceCatastale))
            {
                codiceCatastale = codiceCatastale.Replace(" ", "");
                spec &= new Spec<ComuneItalianoCsv>(t => t.CODICE_CATASTALE.ToUpper() == codiceCatastale.ToUpper());
            }
            if (archiviato != null)
            {
                spec &= new Spec<ComuneItalianoCsv>(t => t.ARCHIVIATO == archiviato);
            }
            return await Task.FromResult(ListaComuniCsv.AsQueryable().Where(spec.Expression).ToList());
        }
    }

}
