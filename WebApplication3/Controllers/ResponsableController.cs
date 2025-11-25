using APIRest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace APIRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResponsableController : Controller
    {
        private string _connectionString;
        public ResponsableController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("Cadena de conexión no encontrada");
            }
        }

        [HttpPost]
        public IActionResult InsertResponsable([FromBody] Responsable request)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SPInsertReponsable", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@idarea", request.idarea);
                        command.Parameters.AddWithValue("@nombre", request.nombre);
                        command.Parameters.AddWithValue("@correo", request.correo);

                        command.ExecuteNonQuery();
                    }
                }

                return Ok("Responsable insertado correctamente");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error SQL: " + ex.Message);
                return StatusCode(500, "Error en la base de datos");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return StatusCode(500, "Error inesperado");
            }
        }

        [HttpGet]
        public IActionResult GetResponsables()
        {
            var responsables = new List<Responsable>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SPListResponsable", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                responsables.Add(new Responsable
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),
                                    nombre = reader.GetString(reader.GetOrdinal("nombre"))
                                });
                            }
                        }
                    }
                }

                return Ok(responsables);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error SQL: " + ex.Message);
                return StatusCode(500, "Error en la base de datos");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return StatusCode(500, "Error inesperado");
            }
        }
    }
}

