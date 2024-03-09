namespace HumanCapitalManagement.Service.Services;
public interface IAzureServiceBus
{
    Task SendMessageAsync<T>(T message, string queueName);
    Task<string> ReceiveMessageAsync<T>(string queueName);
}
