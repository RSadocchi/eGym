using Newtonsoft.Json;

namespace eGym.Application.Model
{
    public class CodiceNome
    {
        [JsonProperty("codice")]
        public string Codice { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}
