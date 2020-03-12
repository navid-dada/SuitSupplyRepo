namespace SuitSupply.Messages.Events
{
    public class OrderFinished : BaseEvent
    {
        public OrderFinished(string orderid)
        {
            OrderId = orderid;
            State = OrderState.Finished;
        }
        
    }
}