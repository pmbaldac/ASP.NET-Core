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
    public class AreaController : Controller
    {

        private readonly string _connectionString;

        public AreaController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("Cadena de conexión no encontrada");
            }
        }

        [HttpGet]
        public IEnumerable<Area> Get()
        {
            var area = new List<Area>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SPListArea", connection))
                    {
                        if (command == null)
                            throw new Exception("El comando es nulo");

                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                area.Add(new Area
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),
                                    area = reader.GetString(reader.GetOrdinal("area"))
                                });
                            }
                        }
                    }
                }

                return area;
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
