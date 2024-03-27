
using System.Text.Json;
using Application.Common.Interfaces;
using Azure.Messaging.ServiceBus;
using checklist_inventory_contracts.Items;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Writers;

namespace Infrastructure.Persistence.ServiceBus;

public class ServiceBusItemDeletedProcessor : BaseHostedService
{
    private readonly ILogger<ServiceBusItemDeletedProcessor> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ServiceBusItemDeletedProcessor(ServiceBusClient serviceBusClient, IServiceScopeFactory serviceScopFactory, ILogger<ServiceBusItemDeletedProcessor> logger, IConfiguration configuration) : base(serviceBusClient, serviceScopFactory, logger, configuration)
    {
        var topicName = configuration.GetSection("ServiceBus")["TopicItemDeleted"] ?? throw new Exception("Missing topic name in configuration");
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

            var itemDeleted = DeserializeObject(args.Message.Body);
            _logger.Log(LogLevel.Information, $"Read itemId: {itemDeleted.itemId} from service bus");

            await itemRepository.DeleteItemById(itemDeleted.itemId);
            _logger.Log(LogLevel.Information, $"Deleted item with id: {itemDeleted.itemId}");
        }

        await args.CompleteMessageAsync(args.Message);
    }

    protected override Task ErrorHandler(ProcessErrorEventArgs args)
    {
        var errorMessage = args.Exception.ToString();
        _logger.Log(LogLevel.Error, errorMessage);
        return Task.CompletedTask;
    }

    protected override ItemDeletedContract DeserializeObject(BinaryData data)
    {
        var ItemDeleted = JsonSerializer.Deserialize<ItemDeletedContract>(data) ?? throw new Exception("Could not serialize from service bus");
        return ItemDeleted;
    }
}