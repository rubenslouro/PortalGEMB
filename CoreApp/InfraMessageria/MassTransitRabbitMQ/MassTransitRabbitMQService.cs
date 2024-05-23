using MassTransit;
using Microsoft.Extensions.Configuration;

namespace InfraMessageria.MassTransitRabbitMQ;

/// <inheritdoc />
public class MassTransitRabbitMQService : IMassTransitRabbitMQService
{
    private readonly IBus _bus;
    private readonly string _connectionsStringRabbitMq;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bus"></param>
    /// <param name="configuration"></param>
    public MassTransitRabbitMQService(
        IBus bus,
        IConfiguration configuration)
    {
        _bus = bus;
        _connectionsStringRabbitMq = configuration["RabbitMQ:ConnectionString"];
        if (string.IsNullOrWhiteSpace(_connectionsStringRabbitMq))
            throw new InvalidOperationException("RabbitMQ:ConnectionString");

    }

    /// <inheritdoc />
    public async Task SendToEndPointAsync<T>(T value, string fila) where T : class
    {
        var uri = new Uri($"{_connectionsStringRabbitMq!}/{fila}");
        var endPoint = await _bus.GetSendEndpoint(uri);
        await endPoint.Send(value);
    }
}