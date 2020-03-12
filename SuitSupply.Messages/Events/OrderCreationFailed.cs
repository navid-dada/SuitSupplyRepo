using System.Collections.Generic;

namespace SuitSupply.Messages.Events
{
    public class OrderCreationFailed : BaseEvent
    {
        public OrderCreationFailed(string id, List<Error> errors)
        {
            Errors = errors;
            OrderId = id;
            State = OrderState.Registered;
        }

        public List<Error> Errors { get; private set; }
    }
}