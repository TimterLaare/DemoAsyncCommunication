using System;

namespace SagaStateMachine.Messages
{
    public record SupplierProcessed(string CustomerEmail, Guid CorrelationId) : IOrderMessage;
}