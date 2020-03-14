using System;
using System.Collections.Generic;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using SuitSupply.Messages;
using SuitSupply.Messages.Commands;
using SuitSupply.Messages.Events;

namespace SuitSupply.Order
{
    public class OrderPaidHandler
    {
        private readonly IBus _bus;
        private readonly IOrderRepository _orderRepository;
        public OrderPaidHandler(IBus bus, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository; 
            _bus = bus;
            _bus.Subscribe("OrderPaid", async (OrderPaidCommand command) =>
            {
                try
                {
                    var order = await _orderRepository.Get( Guid.Parse(command.Id));
                    order.SetAsPaid();
                    await _orderRepository.Update(order);
                    await _bus.PublishAsync(new OrderPaid(command.Id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occured on setting paid order for orderId {command.Id} ,Exception {ex} ");
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