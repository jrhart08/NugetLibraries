using Flurl;
using Flurl.Http;
using Newtonsoft.Json;

namespace MiscLibraries.Flurl;

public class OpenWeatherApiResponse
{
    public record CoordResponse(double Lat, double Lon);

    public record WeatherResponse(int Id, string Main, string Description, string Icon);

    public CoordResponse Coord { get; set; }
    public WeatherResponse[] Weather { get; set; }
}

public class FlurlExamples
{
    readonly string _openWeatherApiKey;
    private readonly HttpClient _httpClient;

    public FlurlExamples(string openWeatherApiKey, IHttpClientFactory httpClientFactory)
    {
        _openWeatherApiKey = openWeatherApiKey;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<string> GetWeatherData(double? lat, double? lon)
    {
        return await "https://api.openweathermap.org/data/2.5/weather"
            // extension methods provided in Flurl package
            .SetQueryParam("lat", lat)
            .SetQueryParam("lon", lon)
            .SetQueryParam("appid", _openWeatherApiKey)
            // extension methods provided in Flurl.Http package
            .GetStringAsync();
    }

    public async Task<OpenWeatherApiResponse> GetWeatherObject(double? lat, double? lon)
    {
        return await "https://api.openweathermap.org/data/2.5/weather"
            // extension methods provided in Flurl package
            .SetQueryParam("lat", lat)
            .SetQueryParam("lon", lon)
            .SetQueryParam("appid", _openWeatherApiKey)
            // extension methods provided in Flurl.Http package
            .GetJsonAsync<OpenWeatherApiResponse>();
    }

    public async Task<string> GetWeatherData_HttpClient(double? lat, double? lon)
    {
        string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={_openWeatherApiKey}";

        var response = await _httpClient.GetAsync(url);

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<OpenWeatherApiResponse> GetWeatherObject_HttpClient(double? lat, double? lon)
    {
        string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={_openWeatherApiKey}";

        var response = await _httpClient.GetAsync(url);

        var json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<OpenWeatherApiResponse>(json);
    }
}