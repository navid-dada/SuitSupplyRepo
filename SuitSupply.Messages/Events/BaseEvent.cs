using System.Dynamic;

namespace SuitSupply.Messages.Events
{
    public class BaseEvent : BaseMessage
    {
        public string OrderId { get; protected set; }
        public OrderState State { get; protected set; } = OrderState.Registered;

        public string RequestId => $"{OrderId}_{State}";

        
    }
}