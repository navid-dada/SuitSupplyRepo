using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;
using Alteration = SuitSupply.Messages.Commands.Alteration;

namespace WebApplication.Services
{
    public interface IOrderService
    {
        Task CreateOrder(OrderInput input);
        Task NotifyOrderPayment(string orderId);
        Task NotifyOrderFinished(string orderId);
        Task<List<OrderVM>> GetOrderList();

        Task<List<Alteration>> GetOrderDetail(string id);
    }
}