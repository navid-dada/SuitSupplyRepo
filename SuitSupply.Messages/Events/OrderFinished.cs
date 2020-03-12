namespace SuitSupply.Messages.Events
{
    public class OrderFinished : BaseEvent
    {
        public OrderFinished(string orderId)
        {
            _orderId = orderId;
            _state = OrderState.Finished;
        }
    }
}