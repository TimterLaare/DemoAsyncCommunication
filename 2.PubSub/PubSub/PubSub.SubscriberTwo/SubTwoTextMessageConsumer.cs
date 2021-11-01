using MassTransit;
using Microsoft.Extensions.Logging;
using PubSub.Messages;
using System.Threading.Tasks;

namespace PubSub.SubscriberTwo
{
    public class SubTwoTextMessageConsumer :
        IConsumer<TextMessage>
    {
        private readonly ILogger<SubTwoTextMessageConsumer> logger;

        public SubTwoTextMessageConsumer(ILogger<SubTwoTextMessageConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<TextMessage> context)
        {
            logger.LogInformation("Received Text: {Text}", context.Message.Text);
        }
    }
}