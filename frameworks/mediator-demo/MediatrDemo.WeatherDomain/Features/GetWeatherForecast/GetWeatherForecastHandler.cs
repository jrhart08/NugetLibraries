using MediatR;
using MediatrDemo.WeatherDomain.Internals;
using Microsoft.Extensions.Logging;

namespace MediatrDemo.WeatherDomain.Features.GetWeatherForecast;

public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastRequest, GetWeatherForecastResponse>
{
    readonly ILogger<GetWeatherForecastHandler> _logger;
    readonly WeatherForecastQuery _forecastQuery;

    public GetWeatherForecastHandler(
        ILogger<GetWeatherForecastHandler> logger,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _forecastQuery = new(httpClientFactory);
    }

    public async Task<GetWeatherForecastResponse> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Fetching weather for zip code: {request.ZipCode}");

        var forecasts = await _forecastQuery.Get(request.ZipCode, request.Days, request.IncludeSummaries);

        return new(forecasts);
    }
}