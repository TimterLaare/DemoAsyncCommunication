using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SagaStateMachine.OrderSaga
{
    public class Program
    {
        private static readonly string databaseId = "saga-state-machine";
        private static readonly string collectionId = "order-saga-states";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var azureServiceBusConnectionString = hostContext.Configuration.GetConnectionString("AzureServiceBus");
                    var cosmosDbUrl = hostContext.Configuration.GetConnectionString("CosmosDbUrl");
                    var cosmosDbKey = hostContext.Configuration.GetConnectionString("CosmosDbKey");
                    
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
                    
                    services.AddHostedService<Worker>();
                });
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureServices((hostContext, services) =>
        //         {
        //             var azureServiceBusConnectionString = hostContext.Configuration.GetConnectionString("AzureServiceBus");
        //             var cosmosDbKey = hostContext.Configuration.GetConnectionString("CosmosDbKey");
        //             var cosmosDbUrl = hostContext.Configuration.GetConnectionString("CosmosDbUrl");
        //             
        //             services.AddHostedService<Worker>();
        //
        //             services.AddSingleton(new CosmosClient(cosmosDbUrl, cosmosDbKey));
        //             services.AddMassTransit(m =>
        //             {
        //                 m.AddConsumersFromNamespaceContaining<Worker>();
        //                 m.SetKebabCaseEndpointNameFormatter();
        //                 // m.AddSagaStateMachine<OrderStateMachine, OrderState>().InMemoryRepository();
        //                 m.AddSagaStateMachine<OrderStateMachine, OrderState>()
        //                     // .InMemoryRepository();
        //                     .CosmosRepository(cosmosDbUrl, cosmosDbKey, r =>
        //                         {
        //                             r.DatabaseId = "masstransit-klad-order-state";
        //                     
        //                             r.CollectionId = "order-sagas";
        //                         }
        //                         // .CosmosRepository(r =>
        //                         //     {
        //                         //         r.ConfigureEmulator();
        //                         //
        //                         //         r.DatabaseId = "test";
        //                         //
        //                         //         r.CollectionId = "sagas";
        //                         //     }
        //                     
        //                     );
        //                 m.UsingAzureServiceBus((context, config) =>
        //                 {
        //                     config.Host(azureServiceBusConnectionString);
        //                     config.ConfigureEndpoints(context);
        //                 });
        //             });
        //
        //             services.AddMassTransitHostedService(true);
        //         });
    }
}