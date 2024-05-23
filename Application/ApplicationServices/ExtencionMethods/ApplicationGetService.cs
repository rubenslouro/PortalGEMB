using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices.ExtencionMethods;

/// <summary>
/// Classe básica com tratamento de erro para gerênciar a captura de instancias injetadas
/// </summary>
public static class ApplicationGetService
{
    /// <summary>
    /// Captura instaância de serviço
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static T Get<T>(this IServiceProvider services) => services.GetService<T>() ?? throw new InvalidOperationException(nameof(T));
}