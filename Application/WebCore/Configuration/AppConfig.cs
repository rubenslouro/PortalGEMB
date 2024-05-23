using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebCore.Configuration;

/// <summary>
/// Classe de configuração de aplicação
/// </summary>
public static class AppConfig
{
    /// <summary>
    /// Método de configuração de aplicação
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public static void AddAppConfig(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseWebOptimizer();
            app.UseExceptionHandler("/Principal/Error");
            app.UseHsts();
        }

        app.UseStatusCodePages();
        app.UseSession();
        app.UseStaticFiles();
        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "Administrativo",
                template: "{area:exists}/{controller=Principal}/{action=Index}");

            //Default Route
            routes.MapRoute(
                name: "default",
                template: "{controller=Principal}/{action=LoginAdm}/{id?}");
        });
    }
}