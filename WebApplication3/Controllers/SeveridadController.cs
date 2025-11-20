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
    public class SeveridadController : Controller
    {
        private readonly string _connectionString;

        public SeveridadController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("Cadena de conexión no encontrada");
            }
        }

        [HttpGet]
        public IEnumerable<Severidad> Get()
        {
            var severidad = new List<Severidad>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SPListSeveridad", connection))
                    {
                        if (command == null)
                            throw new Exception("El comando es nulo");

                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                severidad.Add(new Severidad
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),
                                    severidad = reader.GetString(reader.GetOrdinal("severidad"))
                                });
                            }
                        }
                    }
                }

                return severidad;
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
