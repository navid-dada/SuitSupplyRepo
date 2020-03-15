using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using RabbitMQ.Client.Framing.Impl;
using SuitSupply.Messages;
using SuitSupply.Messages.Commands;
using Alternation = SuitSupply.Order.Domain.Alteration;
using ILogger = NLog.ILogger;

namespace SuitSupply.Order.Controllers
{
    [Route("/api/Order")]
    public class OrderController:Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository, ILogger<OrderController> logger)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }
        
        public class OrderVM
        {
            public string Id { get; set; }
            public string CustomerEmail { get; set; }
            public int State { get; set; }
        }
        [HttpGet]
        [Route("")]
        public async Task<List<OrderVM>> GetAllOrders()
        {
            var result = _orderRepository.GetAll(x=>true).Select(x => new OrderVM
            {
                Id = x.Id.ToString(),
                State = (int)x.State,
                CustomerEmail = x.CustomerEmail,
            }).ToList();
            return result;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IEnumerable<Messages.Commands.Alteration>> GetAllOrders([FromRoute]string id)
        {
            var result = (await _orderRepository.Get(Guid.Parse(id))).Alterations;
            var response = new List<Alteration>();
            foreach (var item in result)
            {
                var size = item.AlterationType == AlterationType.Decreasement
                    ? (-1) * item.AlterationLength
                    : item.AlterationLength;
                response.Add(new Alteration
                {
                    Size = size,
                    Part = item.AlterationPart,
                    Side = item.AlterationSide
                });
            }

            return response;
        }
    }
}