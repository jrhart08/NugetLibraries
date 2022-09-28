using ServiceDemo.Services.Responses;

namespace ServiceDemo.Services.Abstractions;

public interface IWeatherService
{
    Task<List<WeatherForecast>> GetWeatherForecast(string zipCode, int numDays, bool includeSummary = false);
    Task<Dictionary<string, List<WeatherForecast>>> GetWeatherForecastForZipCodes(IEnumerable<string> zipCode, int numDays, bool includeSummary = false);
    
    Task<List<string>> GetSupportedZipCodes();
    Task<List<string>> SendExtremeWeatherAlert(IEnumerable<string> zipCodes, string alertMessage);
}
