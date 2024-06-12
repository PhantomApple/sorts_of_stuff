using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IConfiguration configuration, ILogger<WeatherForecastController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("GetPersons", Name = "GetPersons")]
        public IActionResult GetPersons()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var commandText = "SELECT id_raspes, id_discipline, id_teach, id_group, id_office, id_user, DayNedel, hours_passed FROM Raspes";
                    var command = new SqlCommand(commandText, connection);
                    var reader = command.ExecuteReader();

                    var persons = new List<Person>();

                    while (reader.Read())
                    {
                        var person = new Person
                        {
                            id_raspes = reader.GetInt32(0),
                            id_discipline = reader.GetInt32(1),
                            id_teach = reader.GetInt32(2),
                            id_group = reader.GetInt32(3),
                            id_office = reader.GetInt32(4),
                            id_user = reader.GetInt32(5),
                            DayNedel = reader.GetString(6),
                            hours_passed = reader.GetInt32(7)
                        };
                        persons.Add(person);
                    }

                    return Ok(persons);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data from the database.");
                return StatusCode(500);
            }
        }

        private bool CheckDatabaseConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error establishing connection to the database.");
                return false;
            }
        }

        [HttpGet("CheckConnection", Name = "CheckDatabaseConnection")]
        public IActionResult CheckConnection()
        {
            bool isConnected = CheckDatabaseConnection();
            return Ok(isConnected);
        }
    }
}
