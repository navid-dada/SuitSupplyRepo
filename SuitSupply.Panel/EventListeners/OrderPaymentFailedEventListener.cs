using EasyNetQ;

namespace WebApplication.EventListeners
{
    public class OrderPaymentFailedEventListener
    {
        private IBus _bus;
        public OrderPaymentFailedEventListener(IBus bus)
        {
            _bus = bus;
        }
    }
}