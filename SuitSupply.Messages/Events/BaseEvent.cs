using System.Dynamic;

namespace SuitSupply.Messages.Events
{
    public class BaseEvent
    {
        protected string _orderId { get; set; }
        protected OrderState _state { get; set; } = OrderState.Registered;

        public string RequestId => $"{_orderId}_{_state}";

        public void SetOrderId(string orderId)
        {
            _orderId = orderId;
        }
    }
}