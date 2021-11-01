using MassTransit;
using Microsoft.Extensions.Logging;
using SagaStateMachine.Messages;
using System.Threading.Tasks;

namespace SagaStateMachine.Supplier
{
    public class ProcessSupplierCommandConsumer :
        IConsumer<OrderInitialized>
    {
        private readonly ILogger<ProcessSupplierCommandConsumer> logger;
    
        public ProcessSupplierCommandConsumer(ILogger<ProcessSupplierCommandConsumer> logger)
        {
            this.logger = logger;
        }
    
        public async Task Consume(ConsumeContext<OrderInitialized> context)
        {
            logger.LogInformation($"Processing supplier for customer with e-mail {context.Message.CustomerEmail}");
            await Task.Delay(5000); //mocking doing stuff
    
            logger.LogInformation($"Processing completed!");
            await context.Publish(new SupplierProcessed(context.Message.CustomerEmail, context.Message.CorrelationId));
        }
    }
}