using System;

namespace SuitSupply.Messages.Events
{
    public class OrderPaid : BaseEvent
    {
        public OrderPaid()
        {
            _state = OrderState.Paid;
        }

        
    }
}