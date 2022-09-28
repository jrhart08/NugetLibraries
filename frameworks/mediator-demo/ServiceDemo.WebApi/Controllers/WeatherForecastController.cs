using Microsoft.AspNetCore.Mvc;
using ServiceDemo.Services.Abstractions;
using ServiceDemo.Services.Responses;
using ServiceDemo.WebApi.Requests;

namespace ServiceDemo.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    readonly IWeatherService _weatherService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<WeatherForecast>> Get([FromQuery] GetWeatherForecastRequest request)
    {
        return await _weatherService.GetWeatherForecast(request.ZipCode, request.Days, request.IncludeSummaries);
    }

    [HttpGet("byzipcode")]
    public async Task<Dictionary<string, List<WeatherForecast>>> GetByZipCode([FromQuery] GetWeatherForecastByZipCodeRequest request)
    {
        return await _weatherService.GetWeatherForecastForZipCodes(request.ZipCodes, request.Days, request.IncludeSummaries);
    }

    [HttpGet("zipcodes")]
    public async Task<List<string>> GetSupportedZipCodes()
    {
        return await _weatherService.GetSupportedZipCodes();
    }

    [HttpPost("alert")]
    public async Task<List<string>> SendExtremeWeatherAlert([FromBody] SendExtremeWeatherAlertsRequest request)
    {
        return await _weatherService.SendExtremeWeatherAlert(request.ZipCodes, request.AlertMessage);
    } 
}