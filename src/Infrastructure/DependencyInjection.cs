using Application.Common.Interfaces;
using Application.Punches;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Infrastructure.Persistence.ServiceBus;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MobDeMob.Application.Common.Interfaces;
using MobDeMob.Infrastructure.Repositories;

namespace MobDeMob.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var defaultAzureCredentials = new DefaultAzureCredential();
        services
            .AddPersistence(configuration)
            .AddRepositories()
            .AddApplicationDbContextInitializer()
            .AddAzureClient(configuration, defaultAzureCredentials)
            .AddMemoryCacheService()
            .AddAzureServiceBusSubscription(configuration, defaultAzureCredentials);

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFileStorageRepository, FileStorageRepositories>();
        services.AddScoped<IMobilizationRepository, MobilizationRepository>();
        services.AddScoped<IChecklistCollectionRepository, CheklistCollectionRepository>();
        services.AddScoped<IChecklistRepository, ChecklistRepository>();
        services.AddScoped<IChecklistQuestionRepository, ChecklistQuestionRepository>();
        services.AddScoped<IItemTemplateRepository, ItemTemplateRepository>();
        services.AddScoped<ICacheRepository, CacheRepository>();
        services.AddScoped<IPunchRepository, PunchRepository>();
        services.AddScoped<IItemReposiory, ItemRepository>();
        services.AddScoped<IQuestionTemplateRepository, QuestionTemplateRepository>();


        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ModelContextBase>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlDatabase"), providerOptions => { providerOptions.EnableRetryOnFailure(); }));

        return services;
    }

    public static IServiceCollection AddApplicationDbContextInitializer(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContextInitializer>();
        return services;
    }

    public static IServiceCollection AddAzureClient(this IServiceCollection services, IConfiguration configuration, DefaultAzureCredential azureCredentials)
    {
        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient(new Uri(configuration.GetConnectionString("BlobServiceClientUri") ??
                throw new Exception("Could not find the connectionstring for the storage account uri")));
            builder.UseCredential(azureCredentials);
        });
        return services;
    }

    public static IServiceCollection AddMemoryCacheService(this IServiceCollection services)
    {
        services.AddMemoryCache(options =>
        {
            options.SizeLimit = 1024;
            options.ExpirationScanFrequency = TimeSpan.FromMinutes(30);
        });
        services.AddSingleton<ICacheRepository, CacheRepository>();
        return services;
    }

    public static IServiceCollection AddAzureServiceBusSubscription(this IServiceCollection services, IConfiguration configuration, DefaultAzureCredential azureCredentials)
    {
        services.AddSingleton(x =>
        {
            string serviceBusNamespace = configuration.GetSection("ServiceBus")["Namespace"] ?? throw new Exception("missing namespace in configuration");
            return new ServiceBusClient(string.Concat(serviceBusNamespace,".servicebus.windows.net"), azureCredentials);
        });
        services.AddHostedService<ServiceBusItemCreatedProcessor>();
        services.AddHostedService<ServiceBusItemDeletedProcessor>();

        return services;
    }
}

