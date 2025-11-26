using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using Proyecto2_API.Models;  

namespace Proyecto2_API.Controllers
{
    public class OperacionesController : ApiController
    {
        string connectionString = @"Server=(local);Database=HistorialOperaciones;TrustServerCertificate=true;Integrated Security=SSPI;";


        // --- MÉTODO QUE LEE LA BD Y DEVUELVE LISTA DE OBJETOS ---
        private List<OperacionModel> ObtenerDatos(string sql)
        {
            List<OperacionModel> lista = new List<OperacionModel>();

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conexion);
                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new OperacionModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Operacion = reader["Operacion"].ToString(),
                        Resultado = Convert.ToDouble(reader["Resultado"]),
                        Fecha = Convert.ToDateTime(reader["Fecha"]).ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }
            }
            return lista;
        }

        // --- GET api/operaciones (TODAS) ---
        [HttpGet]
        [Route("api/operaciones")]
        public List<OperacionModel> GetOperaciones()
        {
            string sql = "SELECT * FROM HistorialOperaciones ORDER BY Fecha DESC";
            return ObtenerDatos(sql);
        }

        // --- GET api/sumas ---
        [HttpGet]
        [Route("api/sumas")]
        public List<OperacionModel> GetSumas()
        {
            string sql = "SELECT * FROM HistorialOperaciones WHERE Operacion LIKE '%+%'";
            return ObtenerDatos(sql);
        }

        // --- GET api/restas ---
        [HttpGet]
        [Route("api/restas")]
        public List<OperacionModel> GetRestas()
        {
            string sql = "SELECT * FROM HistorialOperaciones WHERE Operacion LIKE '%-%'";
            return ObtenerDatos(sql);
        }

        // --- GET api/multiplicaciones ---
        [HttpGet]
        [Route("api/multiplicaciones")]
        public List<OperacionModel> GetMultiplicaciones()
        {
            string sql = "SELECT * FROM HistorialOperaciones WHERE Operacion LIKE '%*%'";
            return ObtenerDatos(sql);
        }
        // --- DIVISIONES OBTENIDAS POR DEFAULT EN DB ---
    }

}

