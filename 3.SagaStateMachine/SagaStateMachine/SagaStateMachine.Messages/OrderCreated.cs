using System;

namespace SagaStateMachine.Messages
{
    public record OrderCreated(string CustomerEmail, Guid CorrelationId) : IOrderMessage;

}