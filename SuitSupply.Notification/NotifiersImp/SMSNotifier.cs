using System;
using Microsoft.Extensions.Configuration;
using SuitSupply.Notification.Interfaces;

namespace SuitSupply.Notification.NotifiersImp
{
    public class SMSNotifier :  INotificationSender
    {
        private readonly IConfiguration _config;

        public SMSNotifier(IConfiguration config)
        {
            _config = config;
        }

        public void SendNotification(string recipient, string body)
        {
            Console.WriteLine($"sending text: \r\n {body}  \r\n to {recipient} by email at {DateTime.Now}");
        }
    }
}