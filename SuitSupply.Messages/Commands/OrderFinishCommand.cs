namespace SuitSupply.Messages.Commands
{
    public class OrderFinishedCommand
    {
        public OrderFinishedCommand(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}