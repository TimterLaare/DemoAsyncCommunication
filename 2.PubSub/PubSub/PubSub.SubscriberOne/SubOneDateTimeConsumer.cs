using MassTransit;
using Microsoft.Extensions.Logging;
using PubSub.Messages;
using System.Threading.Tasks;

namespace PubSub.SubscriberOne
{
    public class SubOneDateTimeConsumer : IConsumer<DateTimeMessage>
    {
        private readonly ILogger<SubOneTextMessageConsumer> logger;

        public SubOneDateTimeConsumer(ILogger<SubOneTextMessageConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<DateTimeMessage> context)
        {
            logger.LogInformation($"Received date time message: {context.Message.DateTime}");
        }
    }
}