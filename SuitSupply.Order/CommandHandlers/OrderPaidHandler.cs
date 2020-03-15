using System;
using System.Collections.Generic;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using SuitSupply.Messages;
using SuitSupply.Messages.Commands;
using SuitSupply.Messages.Events;

namespace SuitSupply.Order
{
    public class OrderPaidHandler
    {
        private readonly IBus _bus;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderPaidHandler> _logger; 
        public OrderPaidHandler(IBus bus, IOrderRepository orderRepository, ILogger<OrderPaidHandler> logger)
        {
            _logger = logger;
            _orderRepository = orderRepository; 
            _bus = bus;
            _bus.Subscribe("OrderPaid", async (OrderPaidCommand command) =>
            {
                _logger.LogInformation($"OrderPaid Command has been received for {command.Id} ");
                try
                {
                    var order = await _orderRepository.Get( Guid.Parse(command.Id));
                    order.SetAsPaid();
                    await _orderRepository.Update(order);
                    await _bus.PublishAsync(new OrderPaid(command.Id));
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
                    await _bus.PublishAsync(new OrderPaidFailed(command.Id, errors));
                }
            });
        }
    }
}