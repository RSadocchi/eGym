using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eGym.Application.Model
{

    public class ComuneItalianoJson : CodiceNome
    {
        [JsonProperty("zona")]
        public CodiceNome Zona { get; set; }
        [JsonProperty("regione")]
        public CodiceNome Regione { get; set; }
        [JsonProperty("cm")]
        public CodiceNome CittaMetropolitana { get; set; }
        [JsonProperty("provincia")]
        public CodiceNome Provincia { get; set; }
        [JsonProperty("sigla")]
        public string Sigla { get; set; }
        [JsonProperty("codiceCatastale")]
        public string CodiceCatastale { get; set; }
        [JsonProperty("cap")]
        public List<string> CAP { get; set; }

        public string ProvinciaNome { get { return string.IsNullOrWhiteSpace(Provincia.Nome) ? CittaMetropolitana.Nome : Provincia.Nome; } }
        public string ProvinciaSiglaNome { get { return $"{Sigla} - {ProvinciaNome}"; } }

        public override string ToString() => $"{Sigla} - {Nome}";
    }
}
