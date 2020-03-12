using System;

namespace SuitSupply.Messages.Events
{
    public class OrderCreated:BaseEvent
    {

        public OrderCreated()
        {
            _state = OrderState.Registered;

        }
        
    }
}