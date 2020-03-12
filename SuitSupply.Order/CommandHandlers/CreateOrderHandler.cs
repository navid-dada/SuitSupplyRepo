using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EasyNetQ;
using SuitSupply.Messages;
using SuitSupply.Order.Domain;
using Alternation = SuitSupply.Order.Domain.Alternation;
using AlternationType = SuitSupply.Order.Domain.AlternationType;

namespace SuitSupply.Order
{
    public class CreateOrderHandler
    {
        private readonly IBus _bus;

        public CreateOrderHandler(IBus bus, SuitSupplyContext ctx)
        {
            _bus = bus;
            _bus.Subscribe("CreateOrder", async (CreateOrderCommand command) =>
            {
                try
                {
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

                    ctx.Orders.Add(order);
                    ctx.SaveChanges();
                    await _bus.PublishAsync(new OrderCreated(command.Email));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occured on creating order for Email {command.Email} ,Exception {ex} ");
                    var errors = new List<Error>
                    {
                        new Error
                        {
                            ErrorCode = 101, Message = ex.Message
                        }
                    };
                    await _bus.PublishAsync(new OrderCreationFailed(command.Email, errors));
                }
            });
        }
    }
}