using MediatrDemo.WeatherDomain.SharedResponses;

namespace MediatrDemo.WeatherDomain.Internals;

internal class WeatherForecastQuery
{
    static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public async Task<List<WeatherForecast>> Get(string zipCode, int numDays, bool includeSummary)
    {
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
}