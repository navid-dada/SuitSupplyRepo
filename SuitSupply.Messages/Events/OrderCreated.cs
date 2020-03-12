

namespace SuitSupply.Messages.Events
{
    public class OrderCreated:BaseEvent
    {

        public OrderCreated(string id)
        {
            _orderId = id;
        }
    }
}