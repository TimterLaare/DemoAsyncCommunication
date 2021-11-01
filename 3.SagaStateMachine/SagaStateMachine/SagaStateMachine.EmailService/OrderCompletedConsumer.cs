using MassTransit;
using Microsoft.Extensions.Logging;
using SagaStateMachine.Messages;
using System.Threading.Tasks;

namespace SagaStateMachine.EmailService
{
    public class OrderCompletedConsumer :
        IConsumer<OrderCompleted>
    {
        private readonly ILogger<OrderCompletedConsumer> logger;

        public OrderCompletedConsumer(ILogger<OrderCompletedConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCompleted> context)
        {
            logger.LogInformation($"Sending order completion e-mail to {context.Message.CustomerEmail}");
        }
    }
}