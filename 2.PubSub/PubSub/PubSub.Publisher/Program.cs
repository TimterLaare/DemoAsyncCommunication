using MassTransit;
using Microsoft.Extensions.Configuration;
using PubSub.Messages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PubSub.Publisher
{
    class Program
    {
        private static readonly string AzureServiceBusConnectionString = GetAzureServiceBusConnectionString();

        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingAzureServiceBus(c => c.Host(AzureServiceBusConnectionString));
            await bus.StartAsync();
            try
            {
                while (true)
                {
                    Console.WriteLine(@"a) Publish text message
b) Publish DateTime
q) Quit");
                    var keyChar = char.ToLower(Console.ReadKey(true).KeyChar);

                    switch (keyChar)
                    {
                        case 'a':
                            var textMessage = new TextMessage("Hello world!");
                            await bus.Publish(textMessage);
                            Console.WriteLine("Sent text message!");
                            break;

                        case 'b':
                            var dateTimeMessage = new DateTimeMessage(DateTime.Now);
                            await bus.Publish(dateTimeMessage);
                            Console.WriteLine("Sent date time message!");
                            break;
                        case 'q':
                            goto quit;

                        default:
                            Console.WriteLine($"There's no option ({keyChar})");
                            break;
                    }
                }
            }
            finally
            {
                await bus.StopAsync();
            }
            

            quit:
            Console.WriteLine("Quitting!");
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