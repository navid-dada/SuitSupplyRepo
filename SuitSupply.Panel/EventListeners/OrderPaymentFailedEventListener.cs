using System;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SuitSupply.Messages.Events;
using SuitSupply.SericeBase;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderPaymentFailedEventListener : BaseHandler<OrderPaidFailed>
    {
        private readonly TaskManager _taskManager;
        private readonly ILogger<OrderPaymentFailedEventListener> _logger;

        public OrderPaymentFailedEventListener(IBus bus, TaskManager taskManager, ILogger<OrderPaymentFailedEventListener> logger) : base(bus, "orderPaymentFailedListener")
        {
            _logger = logger;
            _taskManager = taskManager;
        }

        protected override Task OnHandle(OrderPaidFailed message)
        {

            _logger.LogError(
                $"payment for order {message.OrderId} failed with Error{JsonConvert.SerializeObject(message.Errors)}");
            _taskManager.CompleteTask(message.RequestId, false);
            return Task.CompletedTask;
        }
    }
}