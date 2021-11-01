using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SagaStateMachine.Supplier
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

                    services.AddHostedService<Worker>();

                    services.AddMassTransit(m =>
                    {
                        m.AddConsumersFromNamespaceContaining<Worker>();
                        m.SetKebabCaseEndpointNameFormatter();
                        m.UsingAzureServiceBus((context, config) =>
                        {
                            config.Host(azureServiceBusConnectionString);
                            config.ConfigureEndpoints(context);
                        });
                    });

                    services.AddMassTransitHostedService(true);
                });
    }
}