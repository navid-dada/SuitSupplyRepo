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

namespace SuitSupply.Order
{
    public class OrderPaidHandler :BaseHandler<OrderPaidCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderPaidHandler> _logger; 
        public OrderPaidHandler(IBus bus, IOrderRepository orderRepository, ILogger<OrderPaidHandler> logger):base(bus,"OrderPaid")
        {
            _logger = logger;
            _orderRepository = orderRepository; 
        }

        protected override async Task OnHandle(OrderPaidCommand command)
        {
            _logger.LogInformation($"OrderPaid Command has been received for {command.Id} ");
            try
            {
                var order = await _orderRepository.Get( Guid.Parse(command.Id));
                order.SetAsPaid();
                await _orderRepository.Update(order);
                await Bus.PublishAsync(new OrderPaid(command.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured on setting paid order for orderId {command.Id}", ex);
                var errors = new List<Error>
                {
                    new Error
                    {
                        ErrorCode = 101, Message = ex.Message
                    }
                };
                await Bus.PublishAsync(new OrderPaidFailed(command.Id, errors));
            }
        }
    }
}