namespace SuitSupply.Messages.Events
{
    public class OrderPaid : BaseEvent
    {
        public OrderPaid(string orderId)
        {
            _orderId = orderId;
            _state = OrderState.Paid;
        }
    }
}