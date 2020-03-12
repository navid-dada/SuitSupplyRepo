namespace SuitSupply.Messages.Commands
{
    public class OrderPaidCommand
    {
        public OrderPaidCommand(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}