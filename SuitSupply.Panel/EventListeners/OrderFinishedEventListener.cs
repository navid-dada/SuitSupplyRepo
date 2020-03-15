using System;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using SuitSupply.Messages.Events;
using SuitSupply.SericeBase;
using WebApplication.Helper;

namespace WebApplication.EventListeners
{
    public class OrderFinishedEventListener : BaseHandler<OrderFinished>
    {
        private readonly TaskManager _taskManager;
        private readonly ILogger<OrderFinishedEventListener> _logger;

        public OrderFinishedEventListener(IBus bus, TaskManager taskManager, ILogger<OrderFinishedEventListener> logger):base(bus, "orderFinishedListener")
        {
            _logger = logger;
            _taskManager = taskManager;
        }

        protected override Task OnHandle(OrderFinished message)
        {
            
            _logger.LogInformation($"order {message.OrderId} finished successfully ");
            _taskManager.CompleteTask(message.RequestId, true);
            return Task.CompletedTask;

        }
    }
}