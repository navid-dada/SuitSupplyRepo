using SuitSupply.Messages;
using SuitSupply.Order.Domain;

namespace WebApplication.Models
{
    public class OrderVM
    {
        public string Id { get; set; }
        public string CustomerEmail { get; set; }
        public OrderState State { get; set; }
    }
}