using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Phahtshiu.Functions.Infrastructure.Bark.Options;
using Phahtshiu.Functions.Infrastructure.Crawlers.Options;
using Phahtshiu.Functions.Options;
using InfrastructureLineBotOption = Phahtshiu.Functions.Infrastructure.LineBot.Options.LineBotOption;

namespace Phahtshiu.Functions.Configurations;

public static class ConfigurationRegistration
{
    public static IServiceCollection RegisterOptions(
        this IServiceCollection services,
        HostBuilderContext hostContext)
    {
        services.Configure<BarkOption>(hostContext.Configuration.GetSection(BarkOption.Position));
        services.Configure<LineBotOption>(hostContext.Configuration.GetSection(LineBotOption.Position));
        services.Configure<InfrastructureLineBotOption>(hostContext.Configuration.GetSection(LineBotOption.Position));
        services.Configure<ReminderOption>(hostContext.Configuration.GetSection(ReminderOption.Position));
        services.Configure<FeedSourceOption>(hostContext.Configuration.GetSection(FeedSourceOption.Position));
        
        return services;
    }
}