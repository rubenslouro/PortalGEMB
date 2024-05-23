using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfraDatabase.Registers;

/// <summary>
/// Injetor de contexto de dados
/// </summary>
public static class DataContextRegister
{
    /// <summary>
    /// Método que adiciona o contexto de dados
    /// </summary>
    public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectinString = configuration.GetSection("SqlServer").GetSection("ConnectionString").Value;
        services.AddDbContext<CalangoContext>(builder =>
        {
            builder.UseLazyLoadingProxies().UseSqlServer(connectinString ?? throw new InvalidOperationException("ConnectionString"));
        });
    }
}