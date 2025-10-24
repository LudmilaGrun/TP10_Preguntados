using Newtonsoft.Json;
using System.Collections.Generic;

namespace PrimerProyecto.Models
{
    public class Juego
    {
        [JsonProperty]
        public string Username { get; set; }

        [JsonProperty]
        public int PuntajeActual { get; set; }

        [JsonProperty]
        public int CantidadPreguntasCorrectas { get; set; }

        [JsonProperty]
        public int ContadorNroPreguntaActual { get; set; }

        [JsonProperty]
        public Pregunta PreguntaActual { get; set; }

        [JsonProperty]
        public List<Pregunta> ListaPreguntas { get; set; }

        [JsonProperty]
        public List<Respuesta> ListaRespuestas { get; set; }
        public Juego()
        {
            Username = "";
            PuntajeActual = 0;
            CantidadPreguntasCorrectas = 0;
            ContadorNroPreguntaActual = 0;
            PreguntaActual = null;
            ListaPreguntas = new List<Pregunta>();
            ListaRespuestas = new List<Respuesta>();
        }
        public Juego(string username)
        {
            Username = username;
            PuntajeActual = 0;
            CantidadPreguntasCorrectas = 0;
            ContadorNroPreguntaActual = 0;
            PreguntaActual = null;
            ListaPreguntas = new List<Pregunta>();
            ListaRespuestas = new List<Respuesta>();
        }

        private void InicializarJuego()
        {
            Username = "";
            PuntajeActual = 0;
            CantidadPreguntasCorrectas = 0;
            ContadorNroPreguntaActual = 0;
            PreguntaActual = null;
            ListaPreguntas.Clear();
            ListaRespuestas.Clear();
        }

        public List<Categoria> TraerCategorias()
        {
            return BD.TraerCategorias();
        }

        public void CargarPartida(string username, int categoria)
        {
            InicializarJuego();
            Username = username;
            ListaPreguntas = BD.TraerPreguntas(categoria);
            ContadorNroPreguntaActual = 0;
            if (ListaPreguntas.Count > 0)
            {
                PreguntaActual = ListaPreguntas[0];
                ListaRespuestas = BD.TraerRespuestas(PreguntaActual.IdPregunta);
            }
        }

        public Pregunta TraerProximaPregunta()
        {
            if (ContadorNroPreguntaActual < ListaPreguntas.Count)
            {
                PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
                return PreguntaActual;
            }
            else
            {
                return null;
            }
        }

        public List<Respuesta> TraerProximasRespuestas(int idPregunta)
        {
            ListaRespuestas = BD.TraerRespuestas(idPregunta);
            return ListaRespuestas;
        }

        public bool VerificarRespuesta(int idRespuesta)
        {
            bool final = false;
            foreach (Respuesta respuesta in ListaRespuestas)
            {
                if (respuesta.IdRespuesta == idRespuesta)
                {
                    final = respuesta.Correcta;
                    if (final)
                    {
                        PuntajeActual += 10;
                        CantidadPreguntasCorrectas++;
                    }
                }
            }

            ContadorNroPreguntaActual++;
            if (ContadorNroPreguntaActual < ListaPreguntas.Count)
            {
                PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
                ListaRespuestas = BD.TraerRespuestas(PreguntaActual.IdPregunta);
            }
            else
            {
                PreguntaActual = null;
            }

            return final;
        }
    }
}