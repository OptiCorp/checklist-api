
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
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ServiceBusItemCreatedProcessor(ServiceBusClient serviceBusClient, IServiceScopeFactory serviceScopFactory, ILogger<ServiceBusItemCreatedProcessor> logger, IConfiguration configuration) : base(serviceBusClient, serviceScopFactory, logger, configuration) 
    {
        var topicName = configuration.GetSection("ServiceBus")["TopicItemCreated"] ?? throw new Exception("Missing topic name in configuration");
        var subcriptionName = configuration.GetSection("ServiceBus")["SubscriptionName"] ?? throw new Exception("Missing topic name in configuration");

        _logger = logger;
        _serviceBusClient = serviceBusClient;  
        _serviceScopeFactory = serviceScopFactory;
        _processor = _serviceBusClient.CreateProcessor(topicName, subcriptionName, new ServiceBusProcessorOptions());
    }

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