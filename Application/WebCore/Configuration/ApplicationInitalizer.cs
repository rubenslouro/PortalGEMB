using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;
using System;
using ApplicationServices.Registers;
using InfraDatabase.Registers;
using Microsoft.AspNetCore.Builder;
using WebCore.Services.LoginAuthentication;
using Microsoft.AspNetCore.HttpOverrides;

namespace WebCore.Configuration;

/// <summary>
/// Classe de inicialização de aplicação
/// </summary>
public static class ApplicationInitalizer
{
    /// <summary>
    /// Método de inicialização de sistema para API's
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void Init(
        this IServiceCollection services, 
        IConfiguration configuration)
    {

        //services.Configure<IISOptions>(o => { o.ForwardClientCertificate = false; });
        //services.Configure<ForwardedHeadersOptions>(options =>
        //{
        //    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        //    options.KnownNetworks.Clear();
        //    options.KnownProxies.Clear();
        //});


        services.AddResponseCompression();

        services.Configure<GzipCompressionProviderOptions>(options => {
            options.Level = CompressionLevel.Fastest;
        });

        services.AddMvc(delegate (MvcOptions options)
        {
            options.EnableEndpointRouting = false;

        }).AddJsonOptions(
            delegate (JsonOptions options)
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;

            });

        services.AddWebOptimizer();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(2);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        services.AddHttpContextAccessor();

        services.AddScoped<ILoginAuthenticationService, LoginAuthenticationService>();
        services.AddDatabaseContext(configuration);
        services.AddUnitOfWork();
        services.AddApplicationServices();
        
    }
}