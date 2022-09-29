using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using MiscLibraries.Flurl;

namespace MiscLibraries.LazyCacheEx;

public class LazyCacheExamples
{
    readonly FlurlExamples _flurlExamples;
    readonly IAppCache _cache;
    private readonly IMemoryCache _memoryCache;
    
    public LazyCacheExamples(FlurlExamples flurlExamples, IMemoryCache cache)
    {
        _flurlExamples = flurlExamples;
        _cache = new CachingService();
        _memoryCache = cache;
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

    public async Task<OpenWeatherApiResponse> GetCached_Old(double lat, double lon)
    {
        string cacheKey = $"{lat}-{lon}";

        if (_memoryCache.TryGetValue(cacheKey, out OpenWeatherApiResponse value))
        {
            return value;
        }

        var newValue = await _flurlExamples.GetWeatherObject(lat, lon);

        _memoryCache.Set(cacheKey, newValue);

        return newValue;
    }
}
