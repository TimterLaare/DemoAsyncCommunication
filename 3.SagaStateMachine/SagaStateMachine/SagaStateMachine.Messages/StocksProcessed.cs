using System;

namespace SagaStateMachine.Messages
{
    public record StocksProcessed(string CustomerEmail, Guid CorrelationId) : IOrderMessage;
}