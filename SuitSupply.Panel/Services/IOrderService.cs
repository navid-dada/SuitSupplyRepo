using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IOrderService
    {
        Task CreateOrder(OrderInput input);
        Task NotifyOrderPayment(string orderId);
        Task NotifyOrderFinished(string orderId);
        Task<List<OrderVM>> GetOrderList();

    }
}