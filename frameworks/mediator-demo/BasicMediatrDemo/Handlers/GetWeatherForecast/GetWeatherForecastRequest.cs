using MediatR;

namespace BasicMediatrDemo.Handlers.GetWeatherForecast;

public class GetWeatherForecastRequest : IRequest<GetWeatherForecastResponse>
{
    public int Days { get; set; }
}

public class GetWeatherForecastResponse
{

    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
    
    public List<WeatherForecast> Forecasts { get; set; }
}