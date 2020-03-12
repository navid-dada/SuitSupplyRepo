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

        public OrderPaidHandler(IBus bus, SuitSupplyContext ctx)
        {
            _bus = bus;
            _bus.Subscribe("OrderPaid", async (OrderPaidCommand command) =>
            {
                try
                {
                    var order = await ctx.Orders.Include("Alternations").FirstAsync( x=> x.Id == Guid.Parse(command.Id));
                    order.SetAsPaid();
                    ctx.SaveChanges();
                    Console.WriteLine($"Paid Id: {command.Id}");
                    var t = new OrderPaid();
                    t.SetOrderId(command.Id);
                    await _bus.PublishAsync(t);
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