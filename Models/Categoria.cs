using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
namespace PrimerProyecto.Models
{
    public class Categoria
    {
        [JsonProperty]
        public int IdCategoria { get; set; }
        [JsonProperty]
        public string Nombre { get; set; }
        [JsonProperty]
        public string Foto { get; set; }
        public Categoria() { 

        }
        public Categoria(string nombre, string foto)
        {
            Nombre = nombre;
            Foto = foto;
        }

    }
}