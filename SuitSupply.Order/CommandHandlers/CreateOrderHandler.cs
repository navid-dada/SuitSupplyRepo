using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Logging;
using NLog;
using SuitSupply.Messages;
using SuitSupply.Messages.Commands;
using SuitSupply.Messages.Events;
using SuitSupply.Order.Domain;
using SuitSupply.SericeBase;
using Alteration = SuitSupply.Order.Domain.Alteration;


namespace SuitSupply.Order
{
    public class CreateOrderHandler : BaseHandler<CreateOrderCommand>
    {
        private readonly ILogger<CreateOrderHandler> _logger ;
        private IOrderRepository _orderRepository; 
        public CreateOrderHandler(IBus bus, IOrderRepository orderRepository, ILogger<CreateOrderHandler> logger):base(bus,"CreateOrder")
        {
            _logger = logger;
            _orderRepository = orderRepository;
            
        }

        protected override async Task OnHandle(CreateOrderCommand command)
        {
            {
                try
                {
                    _logger.LogInformation($"Create order command received for {command.Email}");
                    var order = new Domain.Order(command.Email);
                    foreach (var alteration in command.Alterations)
                    {
                        var lenght = Math.Abs(alteration.Size);
                        var alterationType =
                            alteration.Size > 0 ? AlterationType.Increscent : AlterationType.Decreasement;
                        if (alteration.Part == AlterationPart.Sleeves)
                        {
                            order.AddAlteration(Alteration.CreateSleeveAlterationInstance(lenght, alteration.Side, alterationType));
                        }
                        else
                        {
                            order.AddAlteration(Alteration.CreateTrousersAlterationInstance(lenght, alteration.Side, alterationType));
                        }
                    }

                    await _orderRepository.Add(order);
                    _logger.LogInformation(
                        $"Order added to database for {order.CustomerEmail} with Alternation count {order.Alterations.Count()} and Id= {order.Id}");
                    
                    await Bus.PublishAsync( new OrderCreated(command.Email));
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        $"Exception occured on creating order for Email {command.Email}", ex);
                    var errors = new List<Error>
                    {
                        new Error
                        {
                            ErrorCode = 101, Message = ex.Message
                        }
                    };
                    await Bus.PublishAsync(new OrderCreationFailed(command.Email, errors));
                }
            }
        }
       
    }
}