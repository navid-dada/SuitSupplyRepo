using System;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using SuitSupply.Messages;
using SuitSupply.Messages.Events;
using SuitSupply.SericeBase;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderCreatedEventListener: BaseHandler<OrderCreated>
    {
        private readonly  TaskManager _taskManager;
        private readonly ILogger<OrderCreatedEventListener> _logger;
        public OrderCreatedEventListener(IBus bus, TaskManager taskManager, ILogger<OrderCreatedEventListener> logger):base(bus, "orderCreatedListener")
        {
            _logger = logger;
            _taskManager = taskManager;
            
        }

        protected override Task OnHandle(OrderCreated message)
        {
            _logger.LogInformation($"Order created successfully for {message.OrderId}");
            _taskManager.CompleteTask(message.RequestId, true);
            return Task.CompletedTask;
        }
    }
}