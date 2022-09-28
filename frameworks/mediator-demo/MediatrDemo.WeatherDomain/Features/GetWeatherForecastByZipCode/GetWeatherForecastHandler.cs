using MediatR;
using MediatrDemo.WeatherDomain.Internals;
using MediatrDemo.WeatherDomain.SharedResponses;
using Microsoft.Extensions.Logging;

namespace MediatrDemo.WeatherDomain.Features.GetWeatherForecastByZipCode;

public class GetWeatherForecastByZipCodeHandler : IRequestHandler<GetWeatherForecastByZipCodeRequest, GetWeatherForecastByZipCodeResponse>
{
    readonly WeatherForecastQuery _query = new();
    readonly ILogger<GetWeatherForecastByZipCodeHandler> _logger;

    public GetWeatherForecastByZipCodeHandler(ILogger<GetWeatherForecastByZipCodeHandler> logger)
    {
        _logger = logger;
    }

    public async Task<GetWeatherForecastByZipCodeResponse> Handle(GetWeatherForecastByZipCodeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Fetching weather for zip code: {request.ZipCodes}");

        var forecastByZip = new Dictionary<string, List<WeatherForecast>>();
        
        // pretend this is parallel
        foreach (string zip in request.ZipCodes)
        {
            var forecast = await _query.Get(zip, request.Days, request.IncludeSummaries);
            
            forecastByZip.Add(zip, forecast);
        }

        return new(forecastByZip);
    }

    readonly record struct ZipForecastQuery(string Zip, Task<List<WeatherForecast>> Query);
}