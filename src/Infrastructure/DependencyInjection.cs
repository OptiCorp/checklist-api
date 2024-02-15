using Application.Common.Interfaces;
using Application.Punches;
using Azure.Identity;
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
        services
            .AddPersistence(configuration)
            .AddRepositories();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFileStorageRepository, FileStorageRepositories>();
        services.AddScoped<IMobilizationRepository, MobilizationRepository>();
        services.AddScoped<IChecklistRepository, CheklistRepository>();
        services.AddScoped<IChecklistItemRepository, ChecklistItemRepository>();
        services.AddScoped<IChecklistItemQuestionRepository, ChecklistItemQuestionRepository>();
        services.AddScoped<ITemplateRepository, TemplateRepository>();
        services.AddScoped<ICacheRepository, CacheRepository>();
        services.AddScoped<IPunchRepository, PunchRepository>();
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

    public static IServiceCollection AddAzureClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient(new Uri(configuration.GetConnectionString("BlobServiceClientUri") ??
                throw new Exception("Could not find the connectionstring for the storage account uri")));
            builder.UseCredential(new DefaultAzureCredential());
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
}

