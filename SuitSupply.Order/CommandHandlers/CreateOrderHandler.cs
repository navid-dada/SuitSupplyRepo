using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EasyNetQ;
using NLog;
using SuitSupply.Messages;
using SuitSupply.Messages.Commands;
using SuitSupply.Messages.Events;
using SuitSupply.Order.Domain;
using Alteration = SuitSupply.Order.Domain.Alteration;


namespace SuitSupply.Order
{
    public class CreateOrderHandler
    {
        private readonly IBus _bus;
        private readonly ILogger _logger = LogManager.GetLogger("CreateOrderHandler");

        public CreateOrderHandler(IBus bus, SuitSupplyContext ctx)
        {
            _bus = bus;
            _bus.Subscribe("CreateOrder", (CreateOrderCommand command) =>
            {
                Console.WriteLine($"reqested on {DateTime.Now.Ticks}");

                try
                {
                    Console.WriteLine($"Create order command recived for {command.Email}");
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
                            order.AddAlteration(
                                Alteration.CreateTrousersAlterationInstance(lenght, alteration.Side,
                                    alterationType));
                        }
                    }

                    Console.WriteLine(
                        $"Adding Order to database {order.CustomerEmail} with Alternation count {order.Alterations.Count()}");
                    ctx.Orders.Add(order);
                    ctx.SaveChanges();
                    
                    
                    _bus.Publish( new OrderCreated(command.Email));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Exception occured on creating order for Email {command.Email} ,Exception {ex} ");
                    var errors = new List<Error>
                    {
                        new Error
                        {
                            ErrorCode = 101, Message = ex.Message
                        }
                    };
                    _bus.Publish(new OrderCreationFailed(command.Email, errors));
                }
            });
        }
    }
}