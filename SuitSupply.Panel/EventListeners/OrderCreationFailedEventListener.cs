using System;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using SuitSupply.Messages.Events;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderCreationFailedEventListener
    {
        private readonly TaskManager _taskManager;
        private readonly IBus _bus;
        private readonly ILogger<OrderCreationFailedEventListener> _logger;

        public OrderCreationFailedEventListener(IBus bus, TaskManager taskManager, ILogger<OrderCreationFailedEventListener> logger)
        {
            _logger = logger;
            _taskManager = taskManager;
            _bus = bus;
            _bus.Subscribe("orderCreationFailedListener", (OrderCreationFailed x) =>
            {
                _logger.LogInformation($"order failed to create for email {x.RequestId}");
                _taskManager.CompleteTask(x.RequestId, false);
            });
        }
    }
}