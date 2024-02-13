
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MobDeMob.Application.Common.Behaviours;

namespace MobDeMob.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(options => {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
        return services;
    }
}