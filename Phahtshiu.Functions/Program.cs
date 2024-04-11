using Microsoft.Extensions.Hosting;
using Phahtshiu.Functions.Application;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostContext, services) =>
    {
        services.RegisterMediatR();
    })
    .Build();

host.Run();