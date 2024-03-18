
// using System.Text.Json;
// using Application.Common.Interfaces;
// using Azure.Messaging.ServiceBus;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using Microsoft.OpenApi.Writers;

// namespace Infrastructure.Persistence.ServiceBus;

// public class ServiceBusItemsCreatedTopicMessageProcessor : ServiceBusMessageProcessor, IHostedService
// {
//     // private readonly ServiceBusClient _serviceBusClient;
//     private readonly ILogger<ServiceBusItemsCreatedTopicMessageProcessor> _logger;

//     private readonly IServiceScopeFactory _serviceScopeFactory;

//     // private readonly ServiceBusProcessor _processor;
//     // private ServiceBusProcessor _processor;
//     // private readonly IItemReposiory _itemReposiory;

//     public ServiceBusItemsCreatedTopicMessageProcessor(ServiceBusClient serviceBusClient, ILogger<ServiceBusItemsCreatedTopicMessageProcessor> logger) : base(serviceBusClient, logger)
//     {
//         _logger = logger;
//         // _serviceScopeFactory = serviceScopFactory;
//         // _topicName = configuration.GetSection("ServiceBus")["TopicName"] ?? throw new Exception("Missing topic name in configuration");
//         // _subcriptionName = configuration.GetSection("ServiceBus")["SubscriptionName"] ?? throw new Exception("Missing topic name in configuration");
//         _processor = _serviceBusClient.CreateProcessor(
//                 configuration.GetSection("ServiceBus")["TopicName"] ?? throw new Exception("Missing topic name in configuration"),
//                 configuration.GetSection("ServiceBus")["SubscriptionName"] ?? throw new Exception("Missing topic name in configuration"),
//                 new ServiceBusProcessorOptions());
//     }

//     public MyCustomServiceBusProcessor(ServiceBusClient serviceBusClient, ILogger<ServiceBusItemsCreatedTopicMessageProcessor> logger)
//     : base(serviceBusClient, logger)
//     {

//         // Initialize any additional dependencies here
//         // _itemRepository = itemRepository;
//     }

//     // public override async Task StartAsync(CancellationToken cancellationToken)
//     // {
//     //     await base.StartAsync(cancellationToken);
//     // }

//     // public override async Task StopAsync(CancellationToken cancellationToken)
//     // {
//     //     await base.StopAsync(cancellationToken);
//     // }
//     // public ServiceBusItemsCreatedTopicMessageProcessor(ServiceBusClient serviceBusClient, IServiceScopeFactory serviceScopFactory, ILogger<ServiceBusItemsCreatedTopicMessageProcessor> logger, IConfiguration configuration,  string topicName, string subscriptionName)
//     // {
//     //     _logger = logger;
//     //     _serviceBusClient = serviceBusClient;
//     //     _serviceScopeFactory = serviceScopFactory;
//     //     _topicName = configuration.GetSection("ServiceBus")["TopicName"] ?? throw new Exception("Missing topic name in configuration");
//     //     _subcriptionName = configuration.GetSection("ServiceBus")["SubscriptionName"] ?? throw new Exception("Missing topic name in configuration");
//     //     _processor = _serviceBusClient.CreateProcessor(_topicName, _subcriptionName, new ServiceBusProcessorOptions());
//     // }

//     // public Task StartAsync(CancellationToken cancellationToken)
//     // {
//     //     _processor.ProcessMessageAsync += MessageHandler;
//     //     _processor.ProcessErrorAsync += ErrorHandler;
//     //     return _processor.StartProcessingAsync(cancellationToken);
//     // }

//     // public async Task StopAsync(CancellationToken cancellationToken)
//     // {
//     //     if (_processor != null)
//     //     {
//     //         await _processor.StopProcessingAsync(cancellationToken);
//     //         await _processor.DisposeAsync();
//     //     }

//     //     if (_serviceBusClient != null)
//     //     {
//     //         await _serviceBusClient.DisposeAsync();
//     //     }
//     // }

//     public override async Task MessageHandler(ProcessMessageEventArgs args)
//     {
//         using (var scope = _serviceScopeFactory.CreateScope())
//         {
//             //string itemId = args.Message.Body.ToString();
//             var itemRepository = scope.ServiceProvider.GetRequiredService<IItemReposiory>();
//             //var itemIdSerializad = TrySerializeMessageData(args.Message.Body);
//             // if (itemIdSerializad != null) await itemRepository.AddItem(itemIdSerializad);
//             var itemId = args.Message.Body.ToString();
//             await itemRepository.AddItem(itemId);


//             _logger.Log(LogLevel.Information, $"Read itemId: {itemId}");
//         }

//         await args.CompleteMessageAsync(args.Message);
//     }

//     // private string? TrySerializeMessageData(BinaryData data)
//     // {
//     //     try
//     //     {

//     //         var serializedItemId = JsonSerializer.Deserialize<string>(data);
//     //         if (serializedItemId == null)
//     //         {
//     //             _logger.Log(LogLevel.Warning, $"Could not serialize data from service bus.\nTried to serielize:{data}");
//     //         }
//     //         return serializedItemId;
//     //     }
//     //     catch (Exception err)
//     //     {
//     //         _logger.Log(LogLevel.Error, $"Could not serialize data from service bus.\nTried to serielize:{data}\nException: {err}");

//     //     }
//     //     return null;

//     // }

//     // private Task ErrorHandler(ProcessErrorEventArgs args)
//     // {
//     //     var errorMessage = args.Exception.ToString();
//     //     _logger.Log(LogLevel.Error, errorMessage);
//     //     return Task.CompletedTask;
//     // }

// }
