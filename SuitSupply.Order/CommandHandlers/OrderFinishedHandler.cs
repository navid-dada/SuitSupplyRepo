using System;
using System.Collections.Generic;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using SuitSupply.Messages;
using SuitSupply.Messages.Commands;
using SuitSupply.Messages.Events;

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
                try
                {
                    var order = await ctx.Orders.Include("Alterations")
                        .FirstAsync(x => x.Id == Guid.Parse(command.Id));
                    order.SetAsFinished();
                    ctx.SaveChanges();
                    var notificationCommand = new NotifyCustomerCommand(order.Id.ToString());
                    notificationCommand.AddNotification(new Notification
                    {
                        Recipient = order.CustomerEmail,
                        NotificationType = NotificationType.Email,
                        Text = $"Your Order is Done!"
                    });
                    await _bus.PublishAsync(notificationCommand);
                    var eve = new OrderFinished(command.Id);
                    Console.WriteLine($"finished {eve.RequestId}");
                    await _bus.PublishAsync(eve);

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
                    await _bus.PublishAsync(new OrderFinishedFailed(command.Id, errors));
                }
            });
        }
    }
}