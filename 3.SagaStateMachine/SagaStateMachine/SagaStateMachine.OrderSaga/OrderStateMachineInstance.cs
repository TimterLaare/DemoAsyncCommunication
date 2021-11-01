using Automatonymous;
using System;

namespace SagaStateMachine.OrderSaga
{
    public class OrderStateMachineInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public string CurrentState { get; set; }

        public string Email { get; set; }

        public int ProcessedEventsState { get; set; }
    }
}