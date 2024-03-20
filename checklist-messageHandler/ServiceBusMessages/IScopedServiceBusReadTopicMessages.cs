
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace checklist_messageHandler.messageReader;


internal interface IScopedServiceBusReadTopicMessages
{
    Task DoWork(CancellationToken cancellationToken);

    Task StopAsync(CancellationToken cancellationToken);
}
public class ScopedServiceBusReadTopicMessages : IScopedServiceBusReadTopicMessages
{
    private readonly ServiceBusClient _serviceBusClient;
    private ServiceBusProcessor _processor;

    private string _topicName;

    private string _subcriptionName;

    private string _serviceBusNamespace;
    public ScopedServiceBusReadTopicMessages(ServiceBusClient serviceBusClient, IConfiguration configuration)
    {
        _serviceBusClient = serviceBusClient;
        _serviceBusNamespace = configuration.GetSection("ServiceBus")["Namespace"] ?? throw new Exception("missing namespace in configuration");
        _topicName = configuration.GetSection("ServiceBus")["TopicName"] ?? throw new Exception("Missing topic name in configuration");
        _subcriptionName = configuration.GetSection("ServiceBus")["SubcriptionName"] ?? throw new Exception("Missing topic name in configuration");
        _processor = _serviceBusClient.CreateProcessor(_topicName, _subcriptionName, new ServiceBusProcessorOptions());
    }

    // public async Task InitProcessor()
    // {
        
    // }

    public async Task DoWork(CancellationToken stoppingToken)
    {

        // while (!stoppingToken.IsCancellationRequested)
        // {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
            await _processor.StartProcessingAsync(stoppingToken);
        // }
    }

    // public Task StartAsync(CancellationToken cancellationToken)
    // {

    //     _processor = _serviceBusClient.CreateProcessor(_topicName, _subcriptionName, new ServiceBusProcessorOptions());

    //     _processor.ProcessMessageAsync += MessageHandler;
    //     _processor.ProcessErrorAsync += ErrorHandler;
    //     return _processor.StartProcessingAsync(cancellationToken);
    // }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_processor != null)
        {
            await _processor.StopProcessingAsync(cancellationToken);
            await _processor.DisposeAsync();
        }

        if (_serviceBusClient != null)
        {
            await _serviceBusClient.DisposeAsync();
        }
    }

    static Task MessageHandler(ProcessMessageEventArgs args)
    {
        string body = args.Message.Body.ToString();
        Console.WriteLine($"$Reccieved: {body}");
        return args.CompleteMessageAsync(args.Message);
    }

    static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }

    // public void Dispose()
    // {
    //     if (_processor != null)
    //     {
    //         _processor.StopProcessingAsync();
    //         _processor.DisposeAsync();
    //     }

    //     if (_serviceBusClient != null)
    //     {
    //         _serviceBusClient.DisposeAsync();
    //     }
    // }
}