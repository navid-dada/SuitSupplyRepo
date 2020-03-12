using EasyNetQ;

namespace WebApplication.EventListeners
{
    public class OrderCreationFailedEventListener
    {
        private IBus _bus;
        public OrderCreationFailedEventListener(IBus bus)
        {
            _bus = bus;
        }
    }
}