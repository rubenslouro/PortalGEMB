using InfraDistributedCache.CacheServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfraDistributedCache.Registers;

/// <summary>
/// Classe de injeção do objeto de gestão do servidor de cache distribuído
/// </summary>
internal static class CacheServerRegister
{
    /// <summary>
    /// Realiza a injeção do objeto de gestão do servidor de cache distribuído
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    internal static void AddDistributedCachingServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:ConnectionString"];
        });

        services.AddSingleton<ICacheServerService, CacheServerService>();
    }
}