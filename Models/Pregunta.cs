using Newtonsoft.Json;

namespace PrimerProyecto.Models
{
    public class Pregunta
    {
        [JsonProperty]
        public int IdPregunta { get; set; }

        [JsonProperty]
        public int IdCategoria { get; set; }

        [JsonProperty]
        public string Enunciado { get; set; }

        [JsonProperty]
        public string Foto { get; set; }
        public Pregunta() {

        }
        public Pregunta(int idCategoria, string enunciado, string foto)
        {
            IdCategoria = idCategoria;
            Enunciado = enunciado;
            Foto = foto;
        }
    }
}