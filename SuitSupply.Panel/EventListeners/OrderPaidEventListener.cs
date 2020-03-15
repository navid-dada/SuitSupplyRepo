using System;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using SuitSupply.Messages.Events;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderPaidEventListener
    {
        private readonly TaskManager _taskManager;
        private readonly IBus _bus;
        private readonly ILogger<OrderPaidEventListener> _logger;

        public OrderPaidEventListener(IBus bus, TaskManager taskManager , ILogger<OrderPaidEventListener> logger)
        {
            _logger = logger;
            _taskManager = taskManager;
            _bus = bus;
            _bus.Subscribe("orderPaidListener", (OrderPaid x) =>
            {
                _logger.LogInformation($"order payment done successfully for {x.OrderId}");
                _taskManager.CompleteTask(x.RequestId, true); 
            });
        }
    }
}