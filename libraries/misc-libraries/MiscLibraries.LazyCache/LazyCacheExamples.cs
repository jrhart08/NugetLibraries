using LazyCache;
using MiscLibraries.Flurl;

namespace MiscLibraries.LazyCacheEx;

public class LazyCacheExamples
{
    readonly FlurlExamples _flurlExamples;
    readonly IAppCache _cache;
    
    public LazyCacheExamples(FlurlExamples flurlExamples)
    {
        _flurlExamples = flurlExamples;
        _cache = new CachingService();
    }

    public async Task<OpenWeatherApiResponse> GetCached(double lat, double lon)
    {
        string cacheKey = $"{lat}-{lon}";

        return await _cache.GetOrAddAsync(cacheKey, async () =>
        {
            await Task.Delay(5000);

            return await _flurlExamples.GetWeatherObject(lat, lon);
        });
    }
}