using Azure.Identity;
using Azure.Messaging.ServiceBus;
using checklist_messageHandler;
using checklist_messageHandler.messageReader;
using MobDeMob.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddPersistence(builder.Configuration);

// builder.Services.AddSingleton(x =>
// {
//     string serviceBusNamespace = builder.Configuration.GetSection("ServiceBus")["Namespace"] ?? throw new Exception("missing namespace in configuration");
//     return new ServiceBusClient(serviceBusNamespace, new DefaultAzureCredential());
// });


builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
builder.Services.AddSingleton<IScopedServiceBusReadTopicMessages, ScopedServiceBusReadTopicMessages>(serviceProvider =>
{
    IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
    string serviceBusNamespace = configuration.GetSection("ServiceBus")["Namespace"] ?? throw new Exception("missing namespace in configuration");
    ServiceBusClient serviceBusClient = new ServiceBusClient(string.Concat(serviceBusNamespace,".servicebus.windows.net"), new DefaultAzureCredential());
    return new ScopedServiceBusReadTopicMessages(serviceBusClient, configuration);
});

var host = builder.Build();
host.Run();
