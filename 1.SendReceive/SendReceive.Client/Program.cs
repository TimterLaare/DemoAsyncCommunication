using MassTransit;
using Microsoft.Extensions.Configuration;
using SendReceive.Messages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SendReceive.Client
{
    class Program
    {
        private static readonly string AzureServiceBusConnectionString = GetAzureServiceBusConnectionString();

        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingAzureServiceBus(c => { c.Host(AzureServiceBusConnectionString); });

            EndpointConvention.Map<TextMessage>(new Uri("queue:text-message"));

            await bus.StartAsync();
            try
            {
                while (true)
                {
                    Console.WriteLine("Type the text you want to send. Type nothing and press enter to quit");
                    var input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input)) break;

                    var message = new TextMessage(input);
                    await bus.Send(message);
                    Console.WriteLine("Text Message sent!");
                }
            }
            finally
            {
                await bus.StopAsync();
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