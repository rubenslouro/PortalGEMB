using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace InfraDistributedCache.CacheServer;

/// <summary>
/// Classe de cache distribuído
/// </summary>
public class CacheServerService : ICacheServerService
{
    private readonly IDistributedCache _distributedCache;
    private readonly string _redisConnectionString;
    /// <summary>
    /// Construtor 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="distributedCache"></param>
    public CacheServerService(
        IConfiguration configuration,
        IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
        _redisConnectionString = configuration["Redis:ConnectionString"] ?? throw new NullReferenceException("Redis:ConnectionString");
    }

    /// <inheritdoc />
    public async Task<bool> CanConnectServer()
    {
        try
        {
            var redis = await ConnectionMultiplexer.ConnectAsync(_redisConnectionString);
            return redis.IsConnected;
        }
        catch
        {
            return false;
        }
    }

    /// <inheritdoc />
    public async Task SetCacheAsync<T>(string key, T value, int maxTimeLimitInMinutes) where T : class
    {
        var optionsCache = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(maxTimeLimitInMinutes))
            .SetSlidingExpiration(TimeSpan.FromMinutes(maxTimeLimitInMinutes));

        var strSerializado = JsonSerializer.Serialize(value);
        var valueInBytes = Encoding.UTF8.GetBytes(strSerializado);

        await _distributedCache.SetAsync(key, valueInBytes, optionsCache);
    }

    /// <inheritdoc />
    public async Task<T?> GetCacheAsync<T>(string key) where T : class
    {
        var valueInBytes = await _distributedCache.GetAsync(key);
        if (valueInBytes == null)
            return null;

        var valueAsString = Encoding.UTF8.GetString(valueInBytes);
        var ret = JsonSerializer.Deserialize<T>(valueAsString);

        return ret;
    }

    /// <inheritdoc />
    public async Task RemoveCacheAsync(string key)
    {
        await _distributedCache.RemoveAsync(key);
    }

}