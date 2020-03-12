using System.Collections.Generic;

namespace SuitSupply.Messages
{
    public class OrderCreationFailed
    {
        public List<string> Errors { get; set; }
    }
}