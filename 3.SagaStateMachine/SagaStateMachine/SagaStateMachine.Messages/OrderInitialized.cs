using System;

namespace SagaStateMachine.Messages
{
    public record OrderInitialized(string CustomerEmail, Guid CorrelationId) : IOrderMessage;
}