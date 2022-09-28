using MediatR;
using MediatrDemo.WeatherDomain.SharedResponses;

namespace MediatrDemo.WeatherDomain.Features.GetWeatherForecast;

public readonly record struct GetWeatherForecastRequest(
    string ZipCode,
    int Days,
    bool IncludeSummaries
) : IRequest<GetWeatherForecastResponse>;
    
public readonly record struct GetWeatherForecastResponse(
    List<WeatherForecast> WeatherForecasts);