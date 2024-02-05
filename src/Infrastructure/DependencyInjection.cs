using MobDeMob.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MobDeMob.Infrastructure.Repositories;
using Application.Common.Interfaces;

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
        services.AddScoped<IPartsRepository, PartsRepository>();
        services.AddScoped<IMobilizationRepository, MobilizationRepository>();
        services.AddScoped<IChecklistRepository, CheklistRepository>();
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
}
