using System;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using SuitSupply.Messages.Events;
using SuitSupply.SericeBase;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderPaidEventListener : BaseHandler<OrderPaid>
    {
        private readonly TaskManager _taskManager;
        private readonly ILogger<OrderPaidEventListener> _logger;

        public OrderPaidEventListener(IBus bus, TaskManager taskManager , ILogger<OrderPaidEventListener> logger) : base(bus,"orderPaidListener")
        {
            _logger = logger;
            _taskManager = taskManager;
        }

        protected override Task OnHandle(OrderPaid message)
        {
            _logger.LogInformation($"order payment done successfully for {message.OrderId}");
            _taskManager.CompleteTask(message.RequestId, true); 
            return Task.CompletedTask;
        }
    }
}