using CommonDependencies;
using Microsoft.Extensions.Logging;
using ServiceDemo.Services.Abstractions;
using ServiceDemo.Services.Responses;

namespace ServiceDemo.Services;

public class WeatherService : IWeatherService
{
    static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    readonly ILogger<WeatherService> _logger;
    readonly MyDbContext _context;
    readonly ITwilioWrapper _twilio;
    readonly IHttpClientFactory _httpClientFactory;

    public WeatherService(
        ILogger<WeatherService> logger,
        MyDbContext context,
        ITwilioWrapper twilio,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _context = context;
        _twilio = twilio;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<WeatherForecast>> GetWeatherForecast(string zipCode, int numDays, bool includeSummary = false)
    {
        return await GetForecastForZipCode(zipCode, numDays, includeSummary);
    }

    public async Task<Dictionary<string, List<WeatherForecast>>> GetWeatherForecastForZipCodes(IEnumerable<string> zipCodes, int numDays, bool includeSummary = false)
    {
        var forecastByZipCode = new Dictionary<string, List<WeatherForecast>>();

        // pretend this is parallel
        foreach (string zip in zipCodes)
        {
            var forecast = await GetForecastForZipCode(zip, numDays, includeSummary);
            forecastByZipCode.Add(zip, forecast);
        }

        return forecastByZipCode;
    }

    public async Task<List<string>> GetSupportedZipCodes()
    {
        return _context
            .SupportedAreas
            .Select(area => area.ZipCode)
            .ToList();
    }

    public async Task<AlertRecipients> SendExtremeWeatherAlert(IEnumerable<string> zipCodes, string alertMessage)
    {
        List<string> phoneNumbersToAlert = _context
            .SupportedAreas
            .Where(area => zipCodes.Contains(area.ZipCode))
            .SelectMany(area => area.Subscribers)
            .Select(sub => sub.PhoneNumber)
            .ToList();

        await _twilio.SendAlert(phoneNumbersToAlert, alertMessage);

        return new(phoneNumbersToAlert);
    }

    #region private methods
    async Task<List<WeatherForecast>> GetForecastForZipCode(string zipCode, int numDays, bool includeSummary)
    {
        // pretend we're using _httpClientFactory here
        
        _logger.LogDebug($"Fetching weather for zip code: {zipCode}");

        await Task.Delay(100);

        return Enumerable
            .Range(1, numDays)
            .Select(index =>
            {
                var forecast = new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                };

                return includeSummary
                    ? forecast with { Summary = Summaries[Random.Shared.Next(Summaries.Length)] }
                    : forecast;
            })
            .ToList();
    }
    #endregion
}