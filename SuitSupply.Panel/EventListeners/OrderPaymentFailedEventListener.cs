using System;
using EasyNetQ;
using SuitSupply.Messages.Events;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderPaymentFailedEventListener
    {
        private TaskManager _taskManager;
        private IBus _bus;
        public OrderPaymentFailedEventListener(IBus bus, TaskManager taskManager)
        {
            _taskManager = taskManager;
            _bus = bus;
            _bus.Subscribe("orderPaymentFailedListener", (OrderPaidFailed x) =>
            {
                _taskManager.CompleteTask(x.RequestId, false);
            });
        }
    }
}