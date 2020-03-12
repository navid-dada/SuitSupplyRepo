using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class OrderInput
    {
        [Required] public string Email { get; set; }
        public List<Alteration> Alterations { get; set; }
    }

    public class Alteration
    {
        public int Side { get; set; }
        public float Size { get; set; }
        public int Part { get; set; }
    }


}