using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.Security;

namespace SuitSupply.Messages.Events
{
    public class OrderPaidFailed : BaseEvent
    {
        public OrderPaidFailed(string orderId, List<Error> errors)
        {
            _orderId = orderId;
            _state = OrderState.Paid;
            Errors = errors;
        }
        public  List<Error> Errors {get; private set;}
    }
}