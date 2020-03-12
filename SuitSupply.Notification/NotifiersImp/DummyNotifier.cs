using System;
using SuitSupply.Notification.Interfaces;

namespace SuitSupply.Notification.NotifiersImp
{
    public class DummyNotifier : INotificationSender
    {
        public void SendNotification(string recipient, string body)
        {
            Console.WriteLine($"Just dummy notifier triggered for {recipient}");
        }
    }
}