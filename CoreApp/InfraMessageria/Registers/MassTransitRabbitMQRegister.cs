using InfraMessageria.MassTransitRabbitMQ;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfraMessageria.Registers;

/// <summary>
/// Classe relacionada a menssageria utilizando MassTransit
/// </summary>
public static class MassTransitRabbitMQRegister
{
    /// <summary>
    /// injeta dependência para producer do MassTransit
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddMassTransitRabbitMqProducer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionsStringRabbitMq = configuration["RabbitMQ:ConnectionString"];
        var userRabbitMq = configuration["RabbitMQ:UserName"];
        var passwordRabbitMq = configuration["RabbitMQ:Password"];

        if (string.IsNullOrWhiteSpace(connectionsStringRabbitMq))
            throw new InvalidOperationException("RabbitMQ:ConnectionString");
        if (string.IsNullOrWhiteSpace(userRabbitMq))
            throw new InvalidOperationException("RabbitMQ:UserName");
        if (string.IsNullOrWhiteSpace(passwordRabbitMq))
            throw new InvalidOperationException("RabbitMQ:Password");

        services.AddMassTransit(obj =>
        {
            obj.AddBus(_ => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(connectionsStringRabbitMq!), h =>
                {
                    h.Username(userRabbitMq);
                    h.Password(passwordRabbitMq);
                });
            }));
        });

        services.AddSingleton<IMassTransitRabbitMQService, MassTransitRabbitMQService>();
    }

}