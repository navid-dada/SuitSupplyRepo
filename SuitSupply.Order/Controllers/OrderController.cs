using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client.Framing.Impl;
using SuitSupply.Messages;
using Alternation = SuitSupply.Order.Domain.Alteration;

namespace SuitSupply.Order.Controllers
{
    [Route("/api/Order")]
    public class OrderController:Controller
    {
        private readonly SuitSupplyContext _context;
        public OrderController(SuitSupplyContext ctx)
        {
            _context = ctx;
        }
        
        public class OrderVM
        {
            public string Id { get; set; }
            public string CustomerEmail { get; set; }
            public int State { get; set; }
        }
        [HttpGet]
        [Route("")]
        public List<OrderVM> GetAllOrders()
        {
            
            var result = _context.Orders.ToList().Select(x => new OrderVM
            {
                Id = x.Id.ToString(),
                State = (int)x.State,
                CustomerEmail = x.CustomerEmail,
            }).ToList();
            return result;
        }
        
        [HttpGet]
        [Route("{id}")]
        public IEnumerable<Alternation> GetAllOrders([FromRoute]string id)
        {
            Console.WriteLine(id);
            var result = _context.Orders.Include("Alterations").First(x=> x.Id == Guid.Parse(id));
            Console.WriteLine(JsonConvert.SerializeObject(result));
            return result.Alterations;
        }
    }
}