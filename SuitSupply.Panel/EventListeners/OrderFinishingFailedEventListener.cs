using System;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using SuitSupply.Messages.Events;
using SuitSupply.Order.Domain;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderFinishingFailedEventListener
    {
        private readonly TaskManager _taskManager;
        private readonly IBus _bus;
        private readonly ILogger<OrderFinishingFailedEventListener> _logger;

        public OrderFinishingFailedEventListener(IBus bus, TaskManager taskManager, ILogger<OrderFinishingFailedEventListener> logger)
        {
            _logger = logger;
            _taskManager = taskManager;
            _bus = bus;
            _bus.Subscribe("orderFinishingFailed", (OrderFinishedFailed x) =>
            {
                _logger.LogError($"Order finishing has been failed for {x.OrderId}");
               _taskManager.CompleteTask(x.RequestId, false);
            });
        }

    }
}