using Microsoft.Extensions.Hosting;
using Phahtshiu.Functions.Applicaiton;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostContext, services) =>
    {
        services.RegisterMediatR();
    })
    .Build();

host.Run();