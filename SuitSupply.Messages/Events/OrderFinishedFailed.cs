using System.Collections.Generic;

namespace SuitSupply.Messages.Events
{
    public class OrderFinishedFailed:BaseEvent
    {
        public OrderFinishedFailed(string orderId, List<Error> errors)
        {
            _orderId = orderId;
            _state = OrderState.Finished;
            Errors = errors;
        }

        public  List<Error> Errors {get;private set;}
    }
}