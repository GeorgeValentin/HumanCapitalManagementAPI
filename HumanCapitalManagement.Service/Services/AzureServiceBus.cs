using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace HumanCapitalManagement.Service.Services;
public class AzureServiceBus : IAzureServiceBus
{
    static ServiceBusClient? client;
    static ServiceBusSender? sender;
    private readonly string connectionString = string.Empty;
    private readonly IConfiguration _configuration;

    public AzureServiceBus(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        connectionString = _configuration["AzureServiceBus:PrimaryConnectionString"];
    }

    public Task<string> ReceiveMessageAsync<T>(string queueName)
    {
        throw new NotImplementedException();
    }

    public async Task SendMessageAsync<T>(T message, string queueName)
    {
        ConfigureData(queueName, connectionString);

        string messageBody = JsonSerializer.Serialize(message);
        ServiceBusMessage messageToSend = 
            new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));

        try
        {
            await sender!.SendMessageAsync(messageToSend);
        }
        finally
        {
            await sender!.DisposeAsync();
            await client!.DisposeAsync();
        }
    }

    private static void ConfigureData(string queueName, string connectionString)
    {
        var clientOptions = new ServiceBusClientOptions()
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
        client = new ServiceBusClient(connectionString, clientOptions);
        sender = client.CreateSender(queueName);
    }
}
