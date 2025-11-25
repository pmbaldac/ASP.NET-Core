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
    public class AuditoriaController : Controller
    {
        private readonly string _connectionString;

        public AuditoriaController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("Cadena de conexión no encontrada");
            }
        }

        [HttpGet]
        public IEnumerable<Auditoria> Get()
        {
            var auditoria = new List<Auditoria>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Select * from viewauditoria", connection))
                    {
                        if (command == null)
                            throw new Exception("El comando es nulo");

                        command.CommandType = CommandType.Text;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                auditoria.Add(new Auditoria
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),
                                    titulo = reader.GetString(reader.GetOrdinal("titulo")),
                                    idresponsable = reader.GetInt32(reader.GetOrdinal("idresponsable")),
                                    responsable = reader.GetString(reader.GetOrdinal("responsable")),
                                    idarea = reader.GetInt32(reader.GetOrdinal("idarea")),
                                    area = reader.GetString(reader.GetOrdinal("area")),
                                    fechainicio = reader.GetDateTime(reader.GetOrdinal("fechainicio")),
                                    fechafin = reader.IsDBNull(reader.GetOrdinal("fechafin"))
                                   ? (DateTime?)null
                                   : reader.GetDateTime(reader.GetOrdinal("fechafin")),
                                    idestado = reader.GetInt32(reader.GetOrdinal("idestado")),
                                    estado = reader.GetString(reader.GetOrdinal("estado"))
                                });
                            }
                        }
                    }
                }

                return auditoria;
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

        [HttpPost]
        public IActionResult InsertAuditoria([FromBody] Auditoria request)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SPInsertAuditoria", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@idarea", request.idarea);
                        command.Parameters.AddWithValue("@titulo", request.titulo);
                        command.Parameters.AddWithValue("@fechainicio", request.fechainicio);

                        // Si fechafin puede ser nulo, controlamos aquí
                        if (request.fechafin.HasValue)
                            command.Parameters.AddWithValue("@fechafin", request.fechafin.Value);
                        else
                            command.Parameters.AddWithValue("@fechafin", DBNull.Value);

                        command.Parameters.AddWithValue("@idresponsable", request.idresponsable);
                        command.Parameters.AddWithValue("@idestado", request.idestado);

                        command.ExecuteNonQuery();
                    }
                }

                return Ok("Auditoría insertada correctamente");
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

        [HttpPut]
        public IActionResult UpdateAuditoria([FromBody] Auditoria request)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SPUpdateAuditoria", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@id", request.id);
                        command.Parameters.AddWithValue("@idarea", request.idarea);
                        command.Parameters.AddWithValue("@titulo", request.titulo);
                        command.Parameters.AddWithValue("@fechainicio", request.fechainicio);

                        if (request.fechafin.HasValue)
                            command.Parameters.AddWithValue("@fechafin", request.fechafin.Value);
                        else
                            command.Parameters.AddWithValue("@fechafin", DBNull.Value);

                        command.Parameters.AddWithValue("@idresponsable", request.idresponsable);
                        command.Parameters.AddWithValue("@idestado", request.idestado);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            return Ok("Auditoría actualizada correctamente");
                        else
                            return NotFound("No se encontró la auditoría con el ID especificado");
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
