using System;
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
            _bus.Subscribe("CreateOrder", (CreateOrderCommand x) =>
            {
                var order = new Domain.Order(x.Email);
                foreach (var alternation in x.Alternations)
                {
                    var lenght = Math.Abs(alternation.Size);
                    var alternationType =
                        alternation.Size > 0 ? AlternationType.Increscent : AlternationType.Decreasement;
                    if (alternation.Part == AlternationPart.Sleeves)
                    {
                        
                        order.AddAlternation(Alternation.CreateSleeveAlternationInstance(lenght,alternation.Side, alternationType));
                    }
                    else
                    {
                        order.AddAlternation(Alternation.CreateTrousersAlternationInstance(lenght,alternation.Side, alternationType));
                    }
                }
                ctx.Orders.Add(order);
                ctx.SaveChanges();
            });
        }
    }
}