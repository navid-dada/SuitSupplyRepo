using System.Collections.Generic;
using SuitSupply.Messages.Events;

namespace SuitSupply.Messages
{
    public class OrderCreationFailed: BaseEvent
    {
        public OrderCreationFailed(string id, List<Error> errors)
        {
            Errors = errors;
            _orderId = id;
            _state = OrderState.Registered;
        }

        public List<Error> Errors { get; private set; }
    }
}