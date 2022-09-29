using System.Diagnostics;
using MiscLibraries.Flurl;
using Polly;
using Polly.Retry;

namespace MiscLibraries.Polly;

public class PollyExamples
{
    // readonly AsyncRetryPolicy _retryPolicy;
    readonly FlurlExamples _flurlExamples;

    public PollyExamples(FlurlExamples flurlExamples)
    {
        _flurlExamples = flurlExamples;
    }

    static readonly AsyncRetryPolicy RetryPolicy = Policy
        .Handle<HttpRequestException>()
        .WaitAndRetryAsync(
            // retry up to 3 times
            3,
            // increase wait time exponentially
            numRetries => TimeSpan.FromSeconds(numRetries ^ 2));

    public async Task<OpenWeatherApiResponse> StubbornlyGetWeather(int lat, int lon)
    {
        return await RetryPolicy.ExecuteAsync(() => _flurlExamples.GetWeatherObject(lat, lon));
    }

    public async Task<OpenWeatherApiResponse> StubbornlyGetWeather_Old(int lat, int lon)
    {
        int numRetries = 0;
        while (numRetries < 3)
        {
            try
            {
                return await _flurlExamples.GetWeatherObject(lat, lon);
            }
            catch (HttpRequestException)
            {
                numRetries++;
                await Task.Delay(1000 * (numRetries ^ 2));

                if (numRetries == 3)
                    throw;
            }
        }

        throw new UnreachableException();
    }
}
