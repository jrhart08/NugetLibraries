using Flurl;
using Flurl.Http;

namespace MiscLibraries.Flurl;

public class FlurlExamples
{
    readonly string _openWeatherApiKey;

    public FlurlExamples(string openWeatherApiKey)
    {
        _openWeatherApiKey = openWeatherApiKey;
    }

    public async Task<string> GetWeatherData(double lat, double lon)
    {
        return await "https://api.openweathermap.org/data/2.5/weather"
            .SetQueryParam("lat", lat)
            .SetQueryParam("lon", lon)
            .SetQueryParam("appid", _openWeatherApiKey)
            // .SetQueryParams(new { lat, lon, appid = _openWeatherApiKey })
            .GetStringAsync();
    }
}