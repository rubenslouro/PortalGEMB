using ApplicationServices.MessageErrors;
using Domain.Interfaces.Exception;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices.Registers;

internal static class ExceptionsRegister
{
    public static void AddExceptions(this IServiceCollection service) 
    {
        service.AddSingleton<IExceptions, Exceptions>();
    }
}