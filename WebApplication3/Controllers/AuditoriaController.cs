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
                                    titulo = reader.GetString(reader.GetOrdinal("titulo")),
                                    responsable = reader.GetString(reader.GetOrdinal("responsable")),
                                    area = reader.GetString(reader.GetOrdinal("area")),
                                    fechainicio = reader.GetDateTime(reader.GetOrdinal("fechainicio")),
                                    fechafin = reader.IsDBNull(reader.GetOrdinal("fechafin"))
                                   ? (DateTime?)null
                                   : reader.GetDateTime(reader.GetOrdinal("fechafin")),
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
    }
}
