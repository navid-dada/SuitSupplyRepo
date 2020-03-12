using System.Collections.Generic;

namespace SuitSupply.Messages.Commands
{
    public class CreateOrderCommand
    {
        public CreateOrderCommand(string email)
        {
            Email = email;
        }

        public string Email { get; private set; }
        public List<Alternation> Alternations { get; set; } = new List<Alternation>();
    }

    public class Alternation
    {
        public AlternationSide Side { get; set; }
        public float Size { get; set; }
        public AlternationPart Part { get; set; }
    }

    
}