using System;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuitSupply.Messages;
using SuitSupply.Messages.Commands;

namespace SuitSupply.Notification
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json", true, true)
                .Build();
            var bus = RabbitHutch.CreateBus("host=localhost;username=admin;password=admin");
            bus.Subscribe("Notifier", (NotifyCustomerCommand x) =>
            {
                var notifierBuilder = new SimpleNotifierFactory(config);
                foreach (var notification in x.Notifications)
                {
                    var notifier = notifierBuilder.CreateNotificationSender(notification.NotificationType);
                    notifier.SendNotification(notification.Recipient, notification.Text);
                }
            });
        }
    }
}