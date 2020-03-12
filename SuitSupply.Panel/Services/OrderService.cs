//using RabbitMQ.Client;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SuitSupply.Messages;
using SuitSupply.Messages.Commands;
using WebApplication.Helper;
using WebApplication.Models;
using Alteration = SuitSupply.Messages.Commands.Alteration;

namespace WebApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBus _channel;
        private readonly IConfiguration _configuration;
        private TaskManager _taskManager;
        public OrderService(IConfiguration configuration,IBus rabbitBus,  TaskManager taskManager)
        {
            _channel = rabbitBus;
            _configuration = configuration;
            _taskManager = taskManager;
        }

        public async Task CreateOrder(OrderInput input)
        {
            var create = new CreateOrderCommand( input.Email);
            
            input.Alterations.ForEach(x =>
            {
                var alt = new Alteration
                {
                    Part = (AlterationPart) x.Part,
                    Size = 3.5f,
                    Side = (AlterationSide) x.Side,
                };
                create.Alterations.Add(alt);
            });
            var taskCompletionSource = _taskManager.AddWaitingTask($"{create.Email}_{OrderState.Registered}");
            await _channel.PublishAsync(create);
            await taskCompletionSource.Task;
        }

        public async Task NotifyOrderPayment(string orderId)
        {
            var taskCompletionSource = _taskManager.AddWaitingTask($"{orderId}_{OrderState.Paid}");
            await _channel.PublishAsync(new OrderPaidCommand(orderId));
            await taskCompletionSource.Task;
        }

        public async Task NotifyOrderFinished(string orderId)
        {
            var taskCompletionSource = _taskManager.AddWaitingTask($"{orderId}_{OrderState.Finished}");
            await _channel.PublishAsync(new OrderFinishedCommand(orderId));
            await taskCompletionSource.Task;
        }

        public async Task<List<OrderVM>> GetOrderList()
        {
            var serviceUrl = _configuration.GetSection("Services")["OrderBaseAddress"];
            var httpclient = new HttpClient();
            var strResponse = await httpclient.GetStringAsync($"{serviceUrl}order/getallorders");
            return JsonConvert.DeserializeObject<List<OrderVM>>(strResponse);
        }
    }
}