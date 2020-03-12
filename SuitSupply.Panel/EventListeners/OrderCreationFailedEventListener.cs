using System;
using EasyNetQ;
using SuitSupply.Messages.Events;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderCreationFailedEventListener
    {
        private TaskManager _taskManager;
        private IBus _bus;
        public OrderCreationFailedEventListener(IBus bus, TaskManager taskManager)
        {
            _taskManager = taskManager;
            _bus = bus;
            _bus.Subscribe("orderCreationFailedListener", (OrderCreationFailed x) =>
            {
                _taskManager.CompleteTask(x.RequestId, false);
            });
        }
    }
}