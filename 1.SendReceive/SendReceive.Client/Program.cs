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
            while (true)
            {
                Console.WriteLine("Type the text you want to send. Type nothing and press enter to quit");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) break;

                var message = new TextMessage(input);
                //todo send text message to queue
                Console.WriteLine("Text Message sent!");
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