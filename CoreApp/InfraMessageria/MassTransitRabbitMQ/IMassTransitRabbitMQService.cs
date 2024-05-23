namespace InfraMessageria.MassTransitRabbitMQ;

/// <summary>
/// Intercafe de ações do MassTransit para RabbitMQ
/// </summary>
public interface IMassTransitRabbitMQService
{
    /// <summary>
    /// Envia objetos para fila de menssageria RabbitMQ
    /// </summary>
    /// <param name="value"></param>
    /// <param name="fila"></param>
    /// <typeparam name="T"></typeparam>
    Task SendToEndPointAsync<T>(T value, string fila) where T : class;
}