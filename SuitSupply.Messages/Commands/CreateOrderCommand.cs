using System.Collections.Generic;

namespace SuitSupply.Messages.Commands
{
    public class CreateOrderCommand : BaseMessage
    {
        public CreateOrderCommand(string email)
        {
            Email = email;
        }

        public string Email { get; private set; }
        public List<Alteration> Alterations { get; set; } = new List<Alteration>();
    }

    public class Alteration
    {
        public AlterationSide Side { get; set; }
        public float Size { get; set; }
        public AlterationPart Part { get; set; }
    }

    
}