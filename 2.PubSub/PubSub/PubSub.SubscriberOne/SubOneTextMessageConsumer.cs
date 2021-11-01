using MassTransit;
using Microsoft.Extensions.Logging;
using PubSub.Messages;
using System.Threading.Tasks;

namespace PubSub.SubscriberOne
{
    public class SubOneTextMessageConsumer : IConsumer<TextMessage>
    {
        private readonly ILogger<SubOneTextMessageConsumer> logger;

        public SubOneTextMessageConsumer(ILogger<SubOneTextMessageConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<TextMessage> context)
        {
            logger.LogInformation($"Received text message: {context.Message.Text}");
        }
    }
}