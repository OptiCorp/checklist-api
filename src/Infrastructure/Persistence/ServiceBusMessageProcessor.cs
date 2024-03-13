
using System.Text.Json;
using Application.Common.Interfaces;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Writers;

namespace Infrastructure.Persistence.ServiceBus;

public class ServiceBusReadTopicMessages : IHostedService
{
    private readonly ILogger<ServiceBusReadTopicMessages> _logger;
    private readonly ServiceBusClient _serviceBusClient;

    private readonly IServiceScopeFactory _serviceScopeFactory;
    private ServiceBusProcessor _processor;
    // private readonly IItemReposiory _itemReposiory;
    private string _topicName;

    private string _subcriptionName;
    public ServiceBusReadTopicMessages(ServiceBusClient serviceBusClient, IServiceScopeFactory serviceScopFactory, ILogger<ServiceBusReadTopicMessages> logger, IConfiguration configuration)
    {
        _logger = logger;
        _serviceBusClient = serviceBusClient;
        _serviceScopeFactory = serviceScopFactory;
        _topicName = configuration.GetSection("ServiceBus")["TopicName"] ?? throw new Exception("Missing topic name in configuration");
        _subcriptionName = configuration.GetSection("ServiceBus")["SubscriptionName"] ?? throw new Exception("Missing topic name in configuration");
        _processor = _serviceBusClient.CreateProcessor(_topicName, _subcriptionName, new ServiceBusProcessorOptions());
    }

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

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            //string itemId = args.Message.Body.ToString();
            var itemRepository = scope.ServiceProvider.GetRequiredService<IItemReposiory>();
            var itemIdSerializad = TrySerializeMessageData(args.Message.Body);
            if (itemIdSerializad != null) await itemRepository.AddItem(itemIdSerializad);

            _logger.Log(LogLevel.Information, $"Read itemId: {itemIdSerializad}");
        }

        await args.CompleteMessageAsync(args.Message);
    }

    private string? TrySerializeMessageData(BinaryData data)
    {
        try
        {

            var serializedItemId = JsonSerializer.Deserialize<string>(data);
            if (serializedItemId == null)
            {
                _logger.Log(LogLevel.Warning, $"Could not serialize data from service bus.\nTried to serielize:{data}");
            }
            return serializedItemId;
        }
        catch (Exception err)
        {
            _logger.Log(LogLevel.Error, $"Could not serialize data from service bus.\nTried to serielize:{data}\nException: {err}");

        }
        return null;

    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        var errorMessage = args.Exception.ToString();
        _logger.Log(LogLevel.Error, errorMessage);
        return Task.CompletedTask;
    }
}