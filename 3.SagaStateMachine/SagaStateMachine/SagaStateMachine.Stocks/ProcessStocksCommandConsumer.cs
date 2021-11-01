using MassTransit;
using Microsoft.Extensions.Logging;
using SagaStateMachine.Messages;
using System.Threading.Tasks;

namespace SagaStateMachine.Stocks
{
    public class ProcessStocksCommandConsumer :
        IConsumer<OrderInitialized>
    {
        private readonly ILogger<ProcessStocksCommandConsumer> logger;
    
        public ProcessStocksCommandConsumer(ILogger<ProcessStocksCommandConsumer> logger)
        {
            this.logger = logger;
        }
    
        public async Task Consume(ConsumeContext<OrderInitialized> context)
        {
            logger.LogInformation($"Processing stocks for customer with e-mail {context.Message.CustomerEmail}");
            await Task.Delay(3000); //mocking doing stuff
    
            logger.LogInformation($"Processing stocks completed!");
            await context.Publish(new StocksProcessed(context.Message.CustomerEmail, context.Message.CorrelationId));
        }
    }
}