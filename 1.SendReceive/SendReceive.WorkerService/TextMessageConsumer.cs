using MassTransit;
using Microsoft.Extensions.Logging;
using SendReceive.Messages;
using System.Threading.Tasks;

namespace SendReceive.WorkerService
{
    public class TextMessageConsumer : IConsumer<TextMessage>
    {
        private readonly ILogger<TextMessageConsumer> logger;

        public TextMessageConsumer(ILogger<TextMessageConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<TextMessage> context)
        {
            // await Task.Delay(3000);
            // throw new Exception("BIEM!");
            logger.LogInformation($"Received text message: {context.Message.Text}");
        }
    }
}