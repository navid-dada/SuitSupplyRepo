using System;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using SuitSupply.Messages.Events;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderFinishedEventListener
    {
        private readonly TaskManager _taskManager;
        private readonly IBus _bus;
        private readonly ILogger<OrderFinishedEventListener> _logger;

        public OrderFinishedEventListener(IBus bus, TaskManager taskManager, ILogger<OrderFinishedEventListener> logger)
        {
            _logger = logger;
            _taskManager = taskManager;
            _bus = bus;
            _bus.Subscribe("orderFinishedListener", (OrderFinished x) =>
            {
                _logger.LogInformation($"order {x.OrderId} finished successfully ");
                _taskManager.CompleteTask(x.RequestId, true);
            });
        }
    }
}