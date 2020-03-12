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
using WebApplication.Models;
using Alternation = SuitSupply.Messages.Commands.Alternation;



namespace WebApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBus _channel;
        private readonly IConfiguration _configuration;
        public OrderService(IConfiguration configuration,IBus rabbitBus)
        {
            _channel = rabbitBus;
            _configuration = configuration;
        }

        public async Task CreateOrder(OrderInput input)
        {
            var create = new CreateOrderCommand();
            create.Email = input.Email;
            input.Alternations.ForEach(x =>
            {
                var alt = new Alternation
                {
                    Part = (AlternationPart) x.Part,
                    Size = 3.5f,
                    Side = (AlternationSide) x.Side,
                };
                create.Alternations.Add(alt);
            });
            await _channel.PublishAsync(create);
        }

        public async Task NotifyOrderPayment(string orderId)
        {
            await _channel.PublishAsync(new OrderPaidCommand{Id = orderId});
        }

        public async Task NotifyOrderFinished(string orderId)
        {
            await _channel.PublishAsync(new OrderFinishedCommand{Id = orderId});
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