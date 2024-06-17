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

        [HttpPost("PostPersons", Name = "PostPersons")]
        public IActionResult PostPersons(int id_discipline, int id_teach, int id_group, int id_office, int id_user, string DayNedel, int hours_passed)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var commandText = "INSERT INTO Raspes ( id_discipline, id_teach, id_group, id_office, id_user, DayNedel, hours_passed) " +
                                      "VALUES ( @id_discipline, @id_teach, @id_group, @id_office, @id_user, @DayNedel, @hours_passed)";
                    var command = new SqlCommand(commandText, connection);

                    // Пример данных для вставки
                    var persons = new List<Person>
                    {
                        new Person { id_discipline = id_discipline, id_teach = id_teach, id_group = id_group, id_office = id_office, id_user = id_user, DayNedel = DayNedel, hours_passed = hours_passed }
                    };

                    foreach (var person in persons)
                    {
                        command.Parameters.Clear();
                        
                        command.Parameters.AddWithValue("@id_discipline", person.id_discipline);
                        command.Parameters.AddWithValue("@id_teach", person.id_teach);
                        command.Parameters.AddWithValue("@id_group", person.id_group);
                        command.Parameters.AddWithValue("@id_office", person.id_office);
                        command.Parameters.AddWithValue("@id_user", person.id_user);
                        command.Parameters.AddWithValue("@DayNedel", person.DayNedel);
                        command.Parameters.AddWithValue("@hours_passed", person.hours_passed);

                        command.ExecuteNonQuery();
                    }

                    return Ok("Data inserted successfully");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data from the database.");
                return StatusCode(501);
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
