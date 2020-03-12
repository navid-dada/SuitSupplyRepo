using EasyNetQ;

namespace WebApplication.EventListeners
{
    public class OrderPaidEventListener
    {
        private IBus _bus;
        public OrderPaidEventListener(IBus bus)
        {
            _bus = bus;
        }
    }
}