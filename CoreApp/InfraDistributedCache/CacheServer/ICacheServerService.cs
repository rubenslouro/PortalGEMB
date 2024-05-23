namespace InfraDistributedCache.CacheServer;

/// <summary>
/// Interface de operações de teste de cache
/// </summary>
public interface ICacheServerService
{
    /// <summary>
    /// Método de teste de conectividade de cache distribuído
    /// </summary>
    /// <returns></returns>
    Task<bool> CanConnectServer();

    /// <summary>
    /// Adiciona registro de cache no servidor de cache distribuído
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="maxTimeLimitInMinutes"></param>
    /// <returns></returns>
    Task SetCacheAsync<T>(string key, T value, int maxTimeLimitInMinutes) where T : class;

    /// <summary>
    /// Recupera registro de cache distribuído
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<T?> GetCacheAsync<T>(string key) where T : class;

    /// <summary>
    /// Remove registro do servidor de cache
    /// </summary>
    /// <param name="key"></param>
    Task RemoveCacheAsync(string key);
}