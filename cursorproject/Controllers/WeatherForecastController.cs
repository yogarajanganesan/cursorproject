using Microsoft.AspNetCore.Mvc;

namespace cursorproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Kishan Y", "Sujan Y", "Lingesh J", "Soundhar S", "Bharathi T", "Jawahar G", "Thiyagarajan B", "Thanarajan C", "Nagarajan R", "Thangarajan R","Yogarajan G","Yogaraja G","Yogaraj G","Rajan G","Raja G","Raj G"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 15).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("Alldata", Name = "GetAllWeatherForecast")]
        public IEnumerable<WeatherForecast> GetAllWeatherForecastduplicate2()
        {
            return Enumerable.Range(1, 15).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
