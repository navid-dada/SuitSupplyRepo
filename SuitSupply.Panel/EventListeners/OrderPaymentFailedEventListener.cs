using System;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SuitSupply.Messages.Events;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderPaymentFailedEventListener
    {
        private readonly TaskManager _taskManager;
        private readonly IBus _bus;
        private readonly ILogger<OrderPaymentFailedEventListener> _logger;

        public OrderPaymentFailedEventListener(IBus bus, TaskManager taskManager, ILogger<OrderPaymentFailedEventListener> logger)
        {
            _logger = logger;
            _taskManager = taskManager;
            _bus = bus;
            _bus.Subscribe("orderPaymentFailedListener", (OrderPaidFailed x) =>
            {
                _logger.LogError($"payment for order {x.OrderId} failed with Error{JsonConvert.SerializeObject(x.Errors)}");
                _taskManager.CompleteTask(x.RequestId, false);
            });
        }
    }
}