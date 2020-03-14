using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuitSupply.Messages;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class OrderController: Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<ActionResult> Index()
        {
            var orderList = await _orderService.GetOrderList();
            return View(orderList);
        }

        public ActionResult NewOrder()
        {
            return View();
        }

        public async Task<ActionResult> Post(OrderInput input)
        {
            await _orderService.CreateOrder(input);
            return Redirect("Index");
        }

        public async Task<ActionResult> SetPaid(string id)
        {
            await _orderService.NotifyOrderPayment(id);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> SetFinished(string id)
        {
            await _orderService.NotifyOrderFinished(id);
            return RedirectToAction("Index");
        }
        
        public async Task<ActionResult> OrderDetail(string id)
        {
            var result = await _orderService.GetOrderDetail(id);
            return View(result);
        }
    }
}