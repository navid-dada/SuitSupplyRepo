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
    public class OrderCreationFailedEventListener : BaseHandler<OrderCreationFailed>
    {
        private readonly TaskManager _taskManager;
        private readonly ILogger<OrderCreationFailedEventListener> _logger;

        public OrderCreationFailedEventListener(IBus bus, TaskManager taskManager, ILogger<OrderCreationFailedEventListener> logger):base(bus, "orderCreationFailedListener")
        {
            _logger = logger;
            _taskManager = taskManager;
        }

        protected override Task OnHandle(OrderCreationFailed message)
        {
            _logger.LogInformation($"order failed to create for email {message.RequestId} with Error{JsonConvert.SerializeObject(message.Errors)}");
            _taskManager.CompleteTask(message.RequestId, false);
            return Task.CompletedTask;
        }
    }
}