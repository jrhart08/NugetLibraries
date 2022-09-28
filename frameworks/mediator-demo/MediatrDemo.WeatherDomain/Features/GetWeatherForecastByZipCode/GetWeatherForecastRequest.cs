using MediatR;
using MediatrDemo.WeatherDomain.SharedResponses;

namespace MediatrDemo.WeatherDomain.Features.GetWeatherForecastByZipCode;

public readonly record struct GetWeatherForecastByZipCodeRequest(
    IEnumerable<string> ZipCodes,
    int Days,
    bool IncludeSummaries
) : IRequest<GetWeatherForecastByZipCodeResponse>;
    
public readonly record struct GetWeatherForecastByZipCodeResponse(
    Dictionary<string, List<WeatherForecast>> WeatherForecasts);