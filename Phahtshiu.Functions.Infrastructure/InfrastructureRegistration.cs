using Microsoft.Extensions.DependencyInjection;
using Phahtshiu.Functions.Application.Sportscenter.Services;
using Phahtshiu.Functions.Infrastructure.Sportscenter;

namespace Phahtshiu.Functions.Infrastructure;

public static class InfrastructureRegistration
{   
    public static IServiceCollection RegisterInfrastructureServices(
        this IServiceCollection services)
    {
        services.AddScoped<ISportscenterService, SportscenterService>();
        return services;
    }
}