using EasyNetQ;
namespace WebApplication.EventListeners
{
    public class OrderCreatedEventListener
    {
        private IBus _bus;
        public OrderCreatedEventListener(IBus bus)
        {
            _bus = bus;
        }
    }
}