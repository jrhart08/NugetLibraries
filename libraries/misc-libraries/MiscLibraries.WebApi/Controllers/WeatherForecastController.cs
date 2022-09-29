// using Microsoft.AspNetCore.Mvc;
// using MiscLibraries.Flurl;
//
// namespace MiscLibraries.WebApi.Controllers;
//
// [ApiController]
// [Route("[controller]")]
// public class WeatherForecastController : ControllerBase
// {
//     private static readonly string[] Summaries = new[]
//     {
//         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//     };
//
//     readonly ILogger<WeatherForecastController> _logger;
//     readonly FlurlExamples _flurlExamples;
//
//     public WeatherForecastController(ILogger<WeatherForecastController> logger, FlurlExamples flurlExamples)
//     {
//         _logger = logger;
//         _flurlExamples = flurlExamples;
//     }
//
//     [HttpGet(Name = "GetWeatherForecast")]
//     public IEnumerable<WeatherForecast> Get()
//     {
//         return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//             {
//                 Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                 TemperatureC = Random.Shared.Next(-20, 55),
//                 Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//             })
//             .ToArray();
//     }
//
//     [HttpGet("realweather")]
//     public async Task<string> GetRealWeather(double latitude, double longitude)
//     {
//         return await _flurlExamples.GetWeatherData(latitude, longitude);
//     }
// }
