using Microsoft.Extensions.DependencyInjection;

namespace Phahtshiu.Functions.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection RegisterMediatR(
        this IServiceCollection services)
    {
        services
            .AddMediatR(config => config
            .RegisterServicesFromAssemblyContaining(typeof(ApplicationRegistration)));
        
        return services;
    }
}