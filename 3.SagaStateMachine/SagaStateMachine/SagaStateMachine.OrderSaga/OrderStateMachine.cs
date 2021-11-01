using Automatonymous;
using Microsoft.Extensions.Logging;
using SagaStateMachine.Messages;

namespace SagaStateMachine.OrderSaga
{
    public class OrderStateMachine : MassTransitStateMachine<OrderStateMachineInstance>
    {
        public OrderStateMachine(ILogger<OrderStateMachine> logger)
        {
            InstanceState(c => c.CurrentState);

            During(Initial,
                When(OrderCreated)
                    .Then(context => logger.LogInformation($"Received Order created with email: {context.Data.CustomerEmail}"))
                    .Then(context => context.Instance.Email = context.Data.CustomerEmail)
                    .Publish(context => new OrderInitialized(context.Data.CustomerEmail, context.Data.CorrelationId))
                    .TransitionTo(Processing)
                );
            
            During(Processing,
                When(StocksProcessed)
                    .Then(c => logger.LogInformation($"Received stocks processed created for email: {c.Data.CustomerEmail}")));
            
            During(Processing,
                When(SupplierProcessed)
                    .Then(c => logger.LogInformation($"Received Supplier processed created for email: {c.Data.CustomerEmail}")));
            
            CompositeEvent(ProcessingCompleted, x => x.ProcessedEventsState, StocksProcessed, SupplierProcessed);
            
            During(Processing,
                When(ProcessingCompleted)
                    .Then(c => logger.LogInformation($"Order completed for email {c.Instance.Email}"))
                    .Publish(context => new OrderCompleted(context.Instance.Email, context.Instance.CorrelationId))
                    .TransitionTo(Final)
                );
            
            SetCompletedWhenFinalized();
            
        }

        public State Processing { get; set; }

        public Event<OrderCreated> OrderCreated { get; set; }
        
        public Event<StocksProcessed> StocksProcessed { get; set; }
        
        public Event<SupplierProcessed> SupplierProcessed { get; set; }

        public Event ProcessingCompleted { get; set; }

    }
}