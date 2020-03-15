using System;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using SuitSupply.Messages.Events;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderCreatedEventListener
    {
        private readonly  IBus _bus;
        private readonly  TaskManager _taskManager;
        private readonly ILogger<OrderCreatedEventListener> _logger;
        public OrderCreatedEventListener(IBus bus, TaskManager taskManager, ILogger<OrderCreatedEventListener> logger)
        {
            _logger = logger;
            _taskManager = taskManager;
            _bus = bus;
            _bus.Subscribe("orderCreatedListener", (OrderCreated x) =>
            {
                _logger.LogInformation($"Order created successfully for {x.OrderId}");
                _taskManager.CompleteTask(x.RequestId, true);
            });
        }
    }
}