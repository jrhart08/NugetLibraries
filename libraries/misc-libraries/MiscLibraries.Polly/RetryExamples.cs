using System.Diagnostics;
using Flurl.Http;
using MiscLibraries.Flurl;
using Polly;
using Polly.Retry;

namespace MiscLibraries.Polly;

public class RetryExamples
{
    // readonly AsyncRetryPolicy _retryPolicy;
    readonly FlurlExamples _flurlExamples;

    public RetryExamples(FlurlExamples flurlExamples)
    {
        _flurlExamples = flurlExamples;
    }

    static readonly AsyncRetryPolicy RetryPolicy = Policy
        .Handle<FlurlHttpException>()
        .WaitAndRetryAsync(
            // retry up to 3 times
            3,
            // increase wait time exponentially
            numRetries => TimeSpan.FromSeconds(Math.Pow(numRetries, 2)));

    public async Task<OpenWeatherApiResponse> StubbornlyGetWeather(double? lat, double? lon)
    {
        return await RetryPolicy.ExecuteAsync(() => _flurlExamples.GetWeatherObject(lat, lon));
    }

    public async Task<OpenWeatherApiResponse> StubbornlyGetWeather_Old(double? lat, double? lon)
    {
        int numRetries = 0;
        while (numRetries < 3)
        {
            try
            {
                return await _flurlExamples.GetWeatherObject(lat, lon);
            }
            catch (FlurlHttpException)
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
