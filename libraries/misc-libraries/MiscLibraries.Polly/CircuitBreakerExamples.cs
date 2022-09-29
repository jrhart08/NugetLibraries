using Flurl.Http;
using MiscLibraries.Flurl;
using Polly;
using Polly.CircuitBreaker;

namespace MiscLibraries.Polly;

public class CircuitBreakerExamples
{
    readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
    readonly FlurlExamples _flurlExamples;

    public CircuitBreakerExamples(FlurlExamples flurlExamples)
    {
        _flurlExamples = flurlExamples;
        _circuitBreakerPolicy = Policy
            .Handle<FlurlHttpException>()
            .CircuitBreakerAsync(
                1,                       // break on first failure
                TimeSpan.FromMinutes(1), // reset breaker after 1 minute
                OnBreak,
                OnReset);
    }

    public async Task<OpenWeatherApiResponse> CautiouslyGetWeather(double? lat, double? lon)
    {
        return await _circuitBreakerPolicy.ExecuteAsync(() => _flurlExamples.GetWeatherObject(lat, lon));
    }

    static void OnBreak(Exception ex, TimeSpan timeSpan) { }

    static void OnReset() { }
}