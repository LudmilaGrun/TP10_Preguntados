using Microsoft.AspNetCore.Mvc;
using PrimerProyecto.Models;
using Microsoft.AspNetCore.Http;

namespace PrimerProyecto.Controllers
{
    public class HomeController : Controller
    {
        private Juego ObtenerJuegoSession()
        {
            string juegO = HttpContext.Session.GetString("Juego");
            if (string.IsNullOrEmpty(juegO))
            {
                return new Juego();
            }
            else
            {
                return Objeto.StringToObject<Juego>(juegO);
            }
        }

        private void GuardarJuegoSession(Juego juego)
        {
            string juegO = Objeto.ObjectToString<Juego>(juego);
            HttpContext.Session.SetString("Juego", juegO);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ConfigurarJuego()
        {
            ViewBag.Categorias = BD.TraerCategorias();
            return View();
        }

        [HttpPost]
        public IActionResult Comenzar(string username, int categoria)
        {
            Juego juego = new Juego();
            juego.CargarPartida(username, categoria);
            GuardarJuegoSession(juego);
            ViewBag.Username = juego.Username;
            ViewBag.PuntajeActual = juego.PuntajeActual;
            ViewBag.ContadorPregunta = juego.ContadorNroPreguntaActual;
            ViewBag.PreguntaActual = juego.PreguntaActual;
            ViewBag.Respuestas = juego.ListaRespuestas;
            return View("Juego");
        }

        public IActionResult Jugar()
        {
            Juego juego = ObtenerJuegoSession();
            if (juego.ContadorNroPreguntaActual >= juego.ListaPreguntas.Count){
                return RedirectToAction("Fin");
            }
            juego.PreguntaActual = juego.TraerProximaPregunta();
            juego.ListaRespuestas = juego.TraerProximasRespuestas(juego.PreguntaActual.IdPregunta);
            GuardarJuegoSession(juego);
            ViewBag.Username = juego.Username;
            ViewBag.PuntajeActual = juego.PuntajeActual;
            ViewBag.ContadorPregunta = juego.ContadorNroPreguntaActual;
            ViewBag.PreguntaActual = juego.PreguntaActual;
            ViewBag.Respuestas = juego.ListaRespuestas;
            return View("Juego");
        }

        [HttpPost]
        public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta)
        {
            Juego juego = ObtenerJuegoSession();
            List<Respuesta> respuestasDeLaPregunta = new List<Respuesta>(juego.ListaRespuestas);
            bool bien = juego.VerificarRespuesta(idRespuesta);
            Respuesta respuestaCorrecta = null;
            foreach (Respuesta respuestas in respuestasDeLaPregunta)
            {
                if (respuestas.Correcta)
                {
                    respuestaCorrecta = respuestas;
                    break;
                }
            }
            if (bien)
            {
                ViewBag.MensajeRespuesta = "¡Correcto!";
            }else if (respuestaCorrecta != null)
            {
                ViewBag.MensajeRespuesta = "Incorrecto. La correcta era: " + respuestaCorrecta.Opcion + ". " + respuestaCorrecta.Contenido;
            }
            else
                {
                    ViewBag.MensajeRespuesta = "Incorrecto.";
                }
            GuardarJuegoSession(juego);
            if (juego.ContadorNroPreguntaActual >= juego.ListaPreguntas.Count){
                return RedirectToAction("Fin");
            }
            juego.PreguntaActual = juego.TraerProximaPregunta();
            juego.ListaRespuestas = juego.TraerProximasRespuestas(juego.PreguntaActual.IdPregunta);
            ViewBag.Username = juego.Username;
            ViewBag.PuntajeActual = juego.PuntajeActual;
            ViewBag.ContadorPregunta = juego.ContadorNroPreguntaActual;
            ViewBag.PreguntaActual = juego.PreguntaActual;
            ViewBag.Respuestas = juego.ListaRespuestas;
            return View("Juego");
        }

        public IActionResult Fin()
        {
            Juego juego = ObtenerJuegoSession();
            ViewBag.Username = juego.Username;
            ViewBag.PuntajeFinal = juego.PuntajeActual;
            HttpContext.Session.Remove("Juego");
            return View();
        }
    }
}
