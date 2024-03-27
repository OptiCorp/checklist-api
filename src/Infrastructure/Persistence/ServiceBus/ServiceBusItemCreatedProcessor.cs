
using System.Text.Json;
using Application.Common.Interfaces;
using Azure.Messaging.ServiceBus;
using Domain.Entities;
using Infrastructure.Persistence.ServiceBus.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Writers;
using MobDeMob.Domain.ItemAggregate;

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
            var itemTemplateRepository = scope.ServiceProvider.GetRequiredService<IItemTemplateRepository>();
            var itemCreated = DeserializeObject(args.Message.Body);
            _logger.Log(LogLevel.Information, $"Read itemId: {itemCreated.itemId} {itemCreated.itemTemplateId}");

            ItemTemplate? itemTemplate;
            itemTemplate = await itemTemplateRepository.GetTemplateById(itemCreated.itemTemplateId);

            itemTemplate ??= await itemTemplateRepository.AddTemplate(ItemTemplate.New(itemCreated.itemTemplateId));

            var item = MapItemCreatedToItem(itemCreated.itemId, itemTemplate.Id);
            await itemRepository.AddItem(item);
        }
        await args.CompleteMessageAsync(args.Message);
    }

    protected override Task ErrorHandler(ProcessErrorEventArgs args)
    {
        var errorMessage = args.Exception.ToString();
        _logger.Log(LogLevel.Error, errorMessage);
        return Task.CompletedTask;
    }

    protected override ItemCreated DeserializeObject(BinaryData data)
    {
        var ItemCreated = JsonSerializer.Deserialize<ItemCreated>(data) ?? throw new Exception("Could not serialize from service bus");
        return ItemCreated;
    }

    private static Item MapItemCreatedToItem(string itemId, string itemTemplateId)
    {
        return Item.New(itemTemplateId, itemId);
    }
}