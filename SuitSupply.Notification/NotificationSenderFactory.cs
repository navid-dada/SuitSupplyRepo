using Microsoft.Extensions.Configuration;
using SuitSupply.Messages;
using SuitSupply.Notification.Interfaces;
using SuitSupply.Notification.NotifiersImp;

namespace SuitSupply.Notification
{
    public class SimpleNotifierFactory
    {
        private readonly IConfiguration _config; 
        public SimpleNotifierFactory(IConfiguration config)
        {
            _config = config;
        }

        public virtual INotificationSender CreateNotificationSender(NotificationType type)
        {
            if (type == NotificationType.Email)
            {
                return new EmailNotifier(_config); 
            }
            else if (type == NotificationType.SMS)
            {
                return new SMSNotifier(_config);
            }

            return new DummyNotifier();
        }
    }

  
}