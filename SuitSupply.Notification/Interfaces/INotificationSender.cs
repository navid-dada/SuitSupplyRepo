namespace SuitSupply.Notification.Interfaces
{
    public interface INotificationSender
    {
        void SendNotification(string recipient, string body); 
    }
}