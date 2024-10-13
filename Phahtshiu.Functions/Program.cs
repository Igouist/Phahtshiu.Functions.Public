using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Phahtshiu.Functions.Application;
using Phahtshiu.Functions.Configurations;
using Phahtshiu.Functions.Infrastructure;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging();
        services.AddHttpClient();
        services.RegisterOptions(hostContext);
        services.RegisterMediatR();
        services.RegisterInfrastructureServices();
    })
    .ConfigureLogging(logging =>
    {
        logging.AddDebug();
        logging.AddConsole();
    })
    .Build();

host.Run();