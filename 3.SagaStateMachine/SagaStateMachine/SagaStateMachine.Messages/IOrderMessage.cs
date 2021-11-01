using MassTransit;
using System;

namespace SagaStateMachine.Messages
{
    public interface IOrderMessage : CorrelatedBy<Guid>
    {
        string CustomerEmail { get; }
    }
}