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
using Alternation = SuitSupply.Order.Domain.Alternation;


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
                    foreach (var alternation in command.Alternations)
                    {
                        var lenght = Math.Abs(alternation.Size);
                        var alternationType =
                            alternation.Size > 0 ? AlternationType.Increscent : AlternationType.Decreasement;
                        if (alternation.Part == AlternationPart.Sleeves)
                        {
                            order.AddAlternation(
                                Alternation.CreateSleeveAlternationInstance(lenght, alternation.Side, alternationType));
                        }
                        else
                        {
                            order.AddAlternation(
                                Alternation.CreateTrousersAlternationInstance(lenght, alternation.Side,
                                    alternationType));
                        }
                    }

                    Console.WriteLine(
                        $"Adding Order to database {order.CustomerEmail} with alternation count {order.Alternations.Count()}");
                    ctx.Orders.Add(order);
                    ctx.SaveChanges();
                    Console.WriteLine($"added on {DateTime.Now.Ticks}");


                    Console.WriteLine($"Order added success fully and event raised by {command.Email}");
                    
                    var eventt = new OrderCreated();
                    eventt.SetOrderId(command.Email);
                    Console.WriteLine($"event raised on {DateTime.Now.Ticks}");

                    _bus.Publish(eventt);
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