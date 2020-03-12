using EasyNetQ;

namespace WebApplication.EventListeners
{
    public class OrderFinishingFailedEventListener
    {
        private IBus _bus;
        public OrderFinishingFailedEventListener(IBus bus)
        {
            _bus = bus;
        }

    }
}