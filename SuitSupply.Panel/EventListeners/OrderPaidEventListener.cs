using System;
using EasyNetQ;
using SuitSupply.Messages.Events;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderPaidEventListener
    {
        private TaskManager _taskManager;
        private IBus _bus;
        public OrderPaidEventListener(IBus bus, TaskManager taskManager)
        {
            _taskManager = taskManager;
            _bus = bus;
            _bus.Subscribe("orderPaidListener", (OrderPaid x) =>
            {
                _taskManager.CompleteTask(x.RequestId, true); 
            });
        }
    }
}