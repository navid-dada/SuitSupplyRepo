using System;
using EasyNetQ;
using SuitSupply.Messages.Events;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderFinishedEventListener
    {
        private TaskManager _taskManager;
        private IBus _bus;
        public OrderFinishedEventListener(IBus bus, TaskManager taskManager)
        {
            _bus = bus;
            _bus.Subscribe("orderFinishedListener", (OrderFinished x) =>
            {
                _taskManager.CompleteTask(x.RequestId, true);
            });
        }
    }
}