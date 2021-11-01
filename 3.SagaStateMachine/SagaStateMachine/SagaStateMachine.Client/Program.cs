using MassTransit;
using Microsoft.Extensions.Configuration;
using SagaStateMachine.Messages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SagaStateMachine.Client
{
    class Program
    {
        private static readonly string AzureServiceBusConnectionString = GetAzureServiceBusConnectionString();

        static async Task Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingAzureServiceBus(c => c.Host(AzureServiceBusConnectionString));
            await busControl.StartAsync();
            try
            {
                while (true)
                {
                    Console.WriteLine("Type the e-mail for the order you want to create. Type nothing and press enter to quit");
                    var email = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(email)) break;

                    var message = new OrderCreated(email, Guid.NewGuid());

                    await busControl.Publish(message);
                    Console.WriteLine("Text Message sent!");
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
        
        private static string GetAzureServiceBusConnectionString() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddUserSecrets<Program>()
                .Build()
                .GetConnectionString("AzureServiceBus");
    }
}