using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace SendReceive.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var azureServiceBusConnectionString = hostContext.Configuration.GetConnectionString("AzureServiceBus");

                    services.AddMassTransit(m =>
                    {
                        m.AddConsumer<TextMessageConsumer>();
                        m.UsingAzureServiceBus((context, config) =>
                        {
                            config.Host(azureServiceBusConnectionString);

                            config.ReceiveEndpoint("text-message", e =>
                            {
                                e.AutoDeleteOnIdle = TimeSpan.FromMinutes(30);
                                e.ConfigureConsumer<TextMessageConsumer>(context);
                            });
                        });
                    });

                    services.AddMassTransitHostedService();
                    services.AddHostedService<Worker>();
                });
    }
}