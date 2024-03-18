
using System.Text.Json;
using Application.Common.Interfaces;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Writers;

namespace Infrastructure.Persistence.ServiceBus;

public class ServiceBusItemCreatedProcessor : BaseHostedService
{
    private readonly ILogger<ServiceBusItemCreatedProcessor> _logger;
    // private readonly ServiceBusClient __serviceBusClient;

    private readonly IServiceScopeFactory _serviceScopeFactory;
    // private readonly ServiceBusProcessor _processor;
    // private readonly IItemReposiory _itemReposiory;
    private readonly string _topicName;

    private readonly string _subcriptionName;
    public ServiceBusItemCreatedProcessor(ServiceBusClient serviceBusClient, IServiceScopeFactory serviceScopFactory, ILogger<ServiceBusItemCreatedProcessor> logger, IConfiguration configuration) 
    {
        _logger = logger;
        _serviceBusClient = serviceBusClient;
        _serviceScopeFactory = serviceScopFactory;
        _topicName = configuration.GetSection("ServiceBus")["TopicName"] ?? throw new Exception("Missing topic name in configuration");
        _subcriptionName = configuration.GetSection("ServiceBus")["SubscriptionName"] ?? throw new Exception("Missing topic name in configuration");
        _processor = _serviceBusClient.CreateProcessor(_topicName, _subcriptionName, new ServiceBusProcessorOptions());
    }

    // public async Task StartAsync(CancellationToken cancellationToken) 
    // {
    //     await base.StartAsync(cancellationToken);
    // }

    // public async Task StopAsync(CancellationToken cancellationToken) 
    // {
    //     await base.StopAsync(cancellationToken);
    // }

    // public Task StartAsync(CancellationToken cancellationToken)
    // {
    //     _processor.ProcessMessageAsync += MessageHandler;
    //     _processor.ProcessErrorAsync += ErrorHandler;
    //     return _processor.StartProcessingAsync(cancellationToken);
    // }

    // public async Task StopAsync(CancellationToken cancellationToken)
    // {
    //     if (_processor != null)
    //     {
    //         await _processor.StopProcessingAsync(cancellationToken);
    //         await _processor.DisposeAsync();
    //     }

    //     if (_serviceBusClient != null)
    //     {
    //         await _serviceBusClient.DisposeAsync();
    //     }
    // }

    protected override async Task MessageHandler(ProcessMessageEventArgs args)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var itemRepository = scope.ServiceProvider.GetRequiredService<IItemReposiory>();
            var itemId = args.Message.Body.ToString();
            await itemRepository.AddItem(itemId);

            _logger.Log(LogLevel.Information, $"Read itemId: {itemId}");
        }

        await args.CompleteMessageAsync(args.Message);
    }

    protected override Task ErrorHandler(ProcessErrorEventArgs args)
    {
        var errorMessage = args.Exception.ToString();
        _logger.Log(LogLevel.Error, errorMessage);
        return Task.CompletedTask;
    }
}