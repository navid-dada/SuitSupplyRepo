using System;
using EasyNetQ;
using SuitSupply.Messages.Events;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderFinishingFailedEventListener
    {
        private TaskManager _taskManager;
        private IBus _bus;
        public OrderFinishingFailedEventListener(IBus bus, TaskManager taskManager)
        {
            _taskManager = taskManager;
            _bus = bus;
            _bus.Subscribe("orderFinishingFailed", (OrderFinishedFailed x) =>
            {
               _taskManager.CompleteTask(x.RequestId, false);
            });
        }

    }
}