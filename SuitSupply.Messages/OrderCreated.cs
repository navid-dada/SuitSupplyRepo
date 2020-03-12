

using SuitSupply.Messages.Events;

namespace SuitSupply.Messages
{
    public class OrderCreated: BaseEvent
    {
        public OrderCreated(string id)
        {
            _orderId = id;
            _state = OrderState.Registered;
        }
    }
}