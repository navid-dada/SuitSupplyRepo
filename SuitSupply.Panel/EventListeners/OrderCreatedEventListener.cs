using System;
using EasyNetQ;
using SuitSupply.Messages.Events;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderCreatedEventListener
    {
        private IBus _bus;
        private TaskManager _taskManager;
        public OrderCreatedEventListener(IBus bus, TaskManager taskManager)
        {

            _taskManager = taskManager;
            _bus = bus;
            _bus.Subscribe("orderCreatedListener", (OrderCreated x) =>
            {
                _taskManager.CompleteTask(x.RequestId, true);
            });
        }
    }
}