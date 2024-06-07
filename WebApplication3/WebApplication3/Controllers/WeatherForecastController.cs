using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;";
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            if (!CheckDatabaseConnection())
            {
                // Handle the case where database connection check fails
                // For example, return an error message
                _logger.LogError(" connection check failed.");
                return new List<WeatherForecast>();
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        private bool CheckDatabaseConnection()
        {
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}