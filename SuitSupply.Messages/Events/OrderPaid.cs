using System;

namespace SuitSupply.Messages.Events
{
    public class OrderPaid : BaseEvent
    {
        public OrderPaid(string orderId)
        {
            OrderId = orderId;
            State = OrderState.Paid;
        }

        
    }
}