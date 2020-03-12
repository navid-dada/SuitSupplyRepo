using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SuitSupply.Messages.Commands
{
    public class NotifyCustomerCommand
    {
        public NotifyCustomerCommand(string orderId)
        {
            OrderId = orderId;
            _notifications = new List<Notification>();
        }

        public string OrderId { get; private set; }
        private List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications {
            get
            {
                return new ReadOnlyCollection<Notification>(_notifications);
            }
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }
    }

    public class Notification
    {
        public NotificationType NotificationType { get; set; }
        public string Text { get; set; }
        public string Recipient { get; set; }
    }
}