using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Phahtshiu.Functions.Options;

namespace Phahtshiu.Functions.Configurations;

public static class ConfigurationRegistration
{
    public static IServiceCollection RegisterOptions(
        this IServiceCollection services,
        HostBuilderContext hostContext)
    {
        services.Configure<LineBotOption>(hostContext.Configuration.GetSection(LineBotOption.Position));
        
        return services;
    }
}