using System;
using System.Collections.Generic;

namespace SuitSupply.Messages
{
    public class CreateOrderCommand
    {
        public string Email { get; set; }
        public List<Alternation> Alternations { get; set; } = new List<Alternation>();
    }

    public class Alternation
    {
        public AlternationSide Side { get; set; }
        public float Size { get; set; }
        public AlternationPart Part { get; set; }
    }


    public enum AlternationSide
    {
        Right =0 ,
        Left =1
    }
    
    public enum AlternationPart
    {
        Sleeves =0 ,
        Trousers =1
    }
}