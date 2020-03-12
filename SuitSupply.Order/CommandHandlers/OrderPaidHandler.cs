using System;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using SuitSupply.Messages;

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
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }
}