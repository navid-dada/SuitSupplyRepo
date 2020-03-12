namespace SuitSupply.Messages.Events
{
    public class OrderFinished : BaseEvent
    {
        public OrderFinished()
        {
            _state = OrderState.Finished;
        }
        
    }
}