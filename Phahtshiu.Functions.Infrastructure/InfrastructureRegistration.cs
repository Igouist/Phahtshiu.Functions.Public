using Microsoft.Extensions.DependencyInjection;
using Phahtshiu.Functions.Application.Notification.Services;
using Phahtshiu.Functions.Application.Sportscenter.Services;
using Phahtshiu.Functions.Infrastructure.Bark;
using Phahtshiu.Functions.Infrastructure.Data;
using Phahtshiu.Functions.Infrastructure.Sportscenter;

namespace Phahtshiu.Functions.Infrastructure;

public static class InfrastructureRegistration
{   
    public static IServiceCollection RegisterInfrastructureServices(
        this IServiceCollection services)
    {
        services.AddSingleton<ITableClientFactory>(provider =>
        {
            var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            return new TableClientFactory(connectionString!);
        });
        
        services.AddScoped<ISportscenterService, SportscenterService>();
        services.AddScoped<INotificationService, BarkNotificationService>();
        return services;
    }
}