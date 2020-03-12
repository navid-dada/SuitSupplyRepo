using System.Collections.Generic;

namespace SuitSupply.Messages.Events
{
    public class OrderCreationFailed : BaseEvent
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