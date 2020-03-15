using System;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SuitSupply.Messages.Events;
using SuitSupply.Order.Domain;
using SuitSupply.SericeBase;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderFinishingFailedEventListener : BaseHandler<OrderFinishedFailed>
    {
        private readonly TaskManager _taskManager;
        private readonly ILogger<OrderFinishingFailedEventListener> _logger;

        public OrderFinishingFailedEventListener(IBus bus, TaskManager taskManager, ILogger<OrderFinishingFailedEventListener> logger)  : base(bus,"orderFinishingFailedListener")
        {
            _logger = logger;
            _taskManager = taskManager;
        }

        protected override Task OnHandle(OrderFinishedFailed message)
        {
            
            _logger.LogError($"Order finishing has been failed for {message.OrderId} with Error{JsonConvert.SerializeObject(message.Errors)}");
            _taskManager.CompleteTask(message.RequestId, false);
            return Task.CompletedTask;
        }
    }
}