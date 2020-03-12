using System;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using SuitSupply.Messages;

namespace SuitSupply.Order
{
    public class OrderFinishedHandler
    {
        private readonly IBus _bus;

        public OrderFinishedHandler(IBus bus, SuitSupplyContext ctx)
        {
            _bus = bus;
            _bus.Subscribe("OrderFinished", async (OrderFinishedCommand command) =>
            {
                var order = await ctx.Orders.Include("Alternations").FirstAsync( x=> x.Id == Guid.Parse(command.Id));
                order.SetAsFinished();
                ctx.SaveChanges();
                var notificationCommand = new NotifyCustomerCommand(order.Id.ToString());
                notificationCommand.AddNotification(new Notification
                    {
                        Recipient = order.CustomerEmail,
                        NotificationType = NotificationType.Email,
                        Text = $"Your Order is Done!"
                    });
                _bus.Publish(notificationCommand);
            });
        }
    }
}