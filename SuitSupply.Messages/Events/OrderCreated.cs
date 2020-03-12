using System;

namespace SuitSupply.Messages.Events
{
    public class OrderCreated:BaseEvent
    {

        public OrderCreated(string orderId)
        {
            State = OrderState.Registered;
            OrderId = orderId;
        }
        
    }
}