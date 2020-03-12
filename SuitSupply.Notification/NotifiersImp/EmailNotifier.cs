using System;
using Microsoft.Extensions.Configuration;
using SuitSupply.Notification.Interfaces;

namespace SuitSupply.Notification.NotifiersImp
{
    public class EmailNotifier : INotificationSender
    {
        private readonly IConfiguration _config;
        public EmailNotifier(IConfiguration config)
        {
            _config = config;
            //Setup notifier
        }

        public void SendNotification(string recipient, string body)
        {
            // this is just for test
            Console.WriteLine($"sending text: \r\n {body}  \r\n to {recipient} by email at {DateTime.Now}");
        }
    }
}