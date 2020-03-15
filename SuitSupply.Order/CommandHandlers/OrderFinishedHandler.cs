using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using SuitSupply.Messages;
using SuitSupply.Messages.Commands;
using SuitSupply.Messages.Events;
using SuitSupply.SericeBase;
using ILogger = NLog.ILogger;

namespace SuitSupply.Order
{
    public class OrderFinishedHandler :BaseHandler<OrderFinishedCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderFinishedHandler> _logger; 

        
        public OrderFinishedHandler(IBus bus, IOrderRepository orderRepository, ILogger<OrderFinishedHandler> logger) : base(bus,"OrderFinished")
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        protected override async Task OnHandle(OrderFinishedCommand command)
        {
            try
            {
                _logger.LogInformation($"FinishOrder Command has been received for {command.Id}");
                    
                var order = await _orderRepository.Get(Guid.Parse(command.Id)); 
                order.SetAsFinished();
                await _orderRepository.Update(order);
                var notificationCommand = new NotifyCustomerCommand(order.Id.ToString());
                notificationCommand.AddNotification(new Notification
                {
                    Recipient = order.CustomerEmail,
                    NotificationType = NotificationType.Email,
                    Text = $"Your Order is Done!"
                });
                await Bus.PublishAsync(notificationCommand);
                    
                _logger.LogInformation($"FinishOrder Command has been received for {command.Id}");
                var eve = new OrderFinished(command.Id);
                    
                _logger.LogInformation($"OrderId {command.Id} has been finished");
                await Bus.PublishAsync(eve);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured on setting paid order for orderId {command.Id} ", ex);
                var errors = new List<Error>
                {
                    new Error
                    {
                        ErrorCode = 101, Message = ex.Message
                    }
                };
                await Bus.PublishAsync(new OrderFinishedFailed(command.Id, errors));
            }
        }
    }
}