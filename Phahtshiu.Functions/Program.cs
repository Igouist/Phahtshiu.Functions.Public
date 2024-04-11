using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Phahtshiu.Functions.Application;
using Phahtshiu.Functions.Configurations;
using Phahtshiu.Functions.Infrastructure;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient();
        services.RegisterOptions(hostContext);
        services.RegisterMediatR();
        services.RegisterInfrastructureServices();
    })
    .Build();

host.Run();