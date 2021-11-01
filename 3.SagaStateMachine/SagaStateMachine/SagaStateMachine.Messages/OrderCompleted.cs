using System;

namespace SagaStateMachine.Messages
{
    public record OrderCompleted(string CustomerEmail, Guid CorrelationId) : IOrderMessage;
}