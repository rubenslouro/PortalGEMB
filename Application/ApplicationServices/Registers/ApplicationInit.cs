using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices.Registers;

public static class ApplicationInit
{
    public static void AddApplicationServices(this IServiceCollection service)
    {
        service.AddExceptions();
        service.AddMappers();
        service.AddDomainServices();
    }
}