using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using OpenTelemetry.Trace;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Oficina.Messaging;

public class ClientMessagingService : IClientMessagingService
{
    private readonly string _hostname = "localhost";
    private readonly string _queueName = "client_messages";
    private static readonly ActivitySource ActivitySource = new("Oficina.Messaging.ClientMessagingService");

    public async Task SendMessage(ClientMessage message)
    {
        using var activity = ActivitySource.StartActivity("SendMessage", ActivityKind.Producer);
        activity?.SetTag("client.id", message.ClientId);
        activity?.SetTag("message.id", message.Id.ToString());
        activity?.SetTag("message.content", message.Content);

        var factory = new ConnectionFactory { HostName = _hostname };

        // Abre conexão e canal (assíncronos na versão nova)
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        // Declara a fila (caso não exista ainda)
        await channel.QueueDeclareAsync(
            queue: _queueName,
            durable: true,      // persiste após restart
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        // Cria mensagem
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        // Define propriedades (persistência)
        var props = new BasicProperties
        {
            ContentType = "application/json",
            DeliveryMode = DeliveryModes.Persistent
        };

        // Publica na fila
        await channel.BasicPublishAsync(
            exchange: "",        // exchange padrão
            routingKey: _queueName,
            mandatory: false,
            basicProperties: props,
            body: body
        );
        Console.WriteLine("teste de log");
    }

    public IEnumerable<ClientMessage> GetMessages(string clientId)
    {
        throw new NotImplementedException("Consumo de mensagens deve ser implementado via consumidor RabbitMQ.");
    }

}

