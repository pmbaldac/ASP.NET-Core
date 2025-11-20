using APIRest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HallazgoController : Controller
    {
        private readonly string _connectionString;

        public HallazgoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("Cadena de conexión no encontrada");
            }
        }

        [HttpGet]
        public IEnumerable<Hallazgo> Get()
        {
            var hallazgo = new List<Hallazgo>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Select * from viewhallazgo", connection))
                    {
                        if (command == null)
                            throw new Exception("El comando es nulo");

                        command.CommandType = CommandType.Text;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hallazgo.Add(new Hallazgo
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),
                                    descripcion = reader.GetString(reader.GetOrdinal("descripcion")),
                                    idauditoria = reader.GetInt32(reader.GetOrdinal("idauditoria")),
                                    titulo = reader.GetString(reader.GetOrdinal("titulo")),
                                    idtipo = reader.GetInt32(reader.GetOrdinal("idtipo")),
                                    tipo = reader.GetString(reader.GetOrdinal("tipo")),
                                    idseveridad = reader.GetInt32(reader.GetOrdinal("idseveridad")),
                                    severidad = reader.GetString(reader.GetOrdinal("severidad")),
                                    fecha = reader.GetDateTime(reader.GetOrdinal("fecha")),
                                    estado = reader.GetString(reader.GetOrdinal("estado"))
                                });
                            }
                        }
                    }
                }

                return hallazgo;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error SQL: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ExecuteReader: " + ex.Message);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SPDeleteHallazgo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            return Ok($"Hallazgo con id {id} eliminado correctamente");
                        else
                            return NotFound($"No se encontró hallazgo con id {id}");
                    }
                }
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
