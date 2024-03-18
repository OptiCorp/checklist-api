using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.ServiceBus;

public abstract class BaseHostedService : IHostedService
{
    protected ServiceBusProcessor _processor;

    protected ServiceBusClient _serviceBusClient;

    // protected string _subcriptionName;

    // protected string _topicName;


    // public BaseHostedService(string subcriptionName, string topicName)
    // {
    //     _subcriptionName = subcriptionName;
    //     _topicName = topicName;
    // }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;
        return _processor.StartProcessingAsync(cancellationToken);
    }

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

    protected abstract Task MessageHandler(ProcessMessageEventArgs args);

    protected abstract Task ErrorHandler(ProcessErrorEventArgs args);

}