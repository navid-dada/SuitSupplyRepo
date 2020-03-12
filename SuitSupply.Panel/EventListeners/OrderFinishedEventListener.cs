using EasyNetQ;

namespace WebApplication.EventListeners
{
    public class OrderFinishedEventListener
    {
        private IBus _bus;
        public OrderFinishedEventListener(IBus bus)
        {
            _bus = bus;
        }
    }
}