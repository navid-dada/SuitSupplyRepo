using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class OrderInput
    {
        [Required] public string Email { get; set; }
        public List<Alternation> Alternations { get; set; }
    }

    public class Alternation
    {
        public int Side { get; set; }
        public float Size { get; set; }
        public int Part { get; set; }
    }


}