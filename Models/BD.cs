using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PrimerProyecto.Models
{
    public static class BD
    {
        private static string _connectionString = @"Server=localhost; DataBase=PreguntadOrt; Integrated Security=True; TrustServerCertificate=True;";
        //private static string _connectionString = @"Server=localhost\SQLEXPRESS;Database=PreguntadOrt;Trusted_Connection=True;Integrated Security=True; TrustServerCertificate=True;";

        
        public static List<Categoria> TraerCategorias()
        {
            List<Categoria> listaCategorias;
            string query = "SELECT * FROM Categorias";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                listaCategorias = connection.Query<Categoria>(query).ToList();
            }
            return listaCategorias;
        }

        public static List<Pregunta> TraerPreguntas(int IdCategoria)
        {
            List<Pregunta> listaPreguntas;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (IdCategoria == -1)
                {
                    string query = "SELECT * FROM Preguntas";
                    listaPreguntas = connection.Query<Pregunta>(query).ToList();
                }
                else
                {
                    string query = "SELECT * FROM Preguntas WHERE IdCategoria = @pIdCategoria";
                    listaPreguntas = connection.Query<Pregunta>(query, new { pIdCategoria = IdCategoria }).ToList();
                }
            }
            return listaPreguntas;
        }

        public static List<Respuesta> TraerRespuestas(int IdPregunta)
        {
            List<Respuesta> listaRespuestas;
            string query = "SELECT * FROM Respuestas WHERE IdPregunta = @pIdPregunta";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                listaRespuestas = connection.Query<Respuesta>(query, new { pIdPregunta = IdPregunta }).ToList();
            }
            return listaRespuestas;
        }
    }
}