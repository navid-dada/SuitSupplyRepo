using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using SuitSupply.Messages;
using SuitSupply.Order.Domain;
using Alternation = SuitSupply.Order.Domain.Alternation;

namespace SuitSupplyTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void order_add_two_same_side_and_part_alt()
        {
            float finalLenght = 3.5f; 
            var order = new Order("Email@site.com");
            order.AddAlternation(Alternation.CreateTrousersAlternationInstance(5, AlternationSide.Left,AlternationType.Decreasement));
            order.AddAlternation(Alternation.CreateTrousersAlternationInstance(finalLenght, AlternationSide.Left, AlternationType.Decreasement));
            Assert.True(order.Alternations.Count() == 1);
            var item = order.Alternations.First();
            Assert.AreEqual(finalLenght, item.AlternationLength);
        }
        
        [Test]
        public void order_add_two_different_side_alt()
        {
            var left_length = 4.5f;
            var right_length = 3.5f;
            var order = new Order("Email@site.com");
            order.AddAlternation(Alternation.CreateTrousersAlternationInstance(left_length, AlternationSide.Left,AlternationType.Decreasement));
            order.AddAlternation(Alternation.CreateTrousersAlternationInstance(right_length, AlternationSide.Right, AlternationType.Increscent));
            Assert.True(order.Alternations.Count() == 2);
            var rightItem = order.Alternations.First(x=>x.AlternationSide== AlternationSide.Right);
            Assert.AreEqual(right_length, rightItem.AlternationLength);
            
            var leftItem = order.Alternations.First(x=>x.AlternationSide== AlternationSide.Right);
            Assert.AreEqual(right_length, leftItem.AlternationLength);
        }

        [Test]
        public void order_add_alt_with_length_greater_than_5()
        {
            var length = 5.1f;
            var order = new Order("Email@site.com");

            void TestDelegate() => order.AddAlternation(Alternation.CreateTrousersAlternationInstance(length, AlternationSide.Left, AlternationType.Decreasement));
            Assert.Throws<InvalidOperationException>(TestDelegate); 
        }
        
        [Test]
        public void order_add_alt_with_length_less_than_0()
        {
            var length = -0.1f;
            var order = new Order("Email@site.com");

            void TestDelegate() => order.AddAlternation(Alternation.CreateTrousersAlternationInstance(length, AlternationSide.Left, AlternationType.Decreasement));
            Assert.Throws<InvalidOperationException>(TestDelegate); 
        }

        [Test]
        public void order_set_paid_with_no_alternation()
        {
            var order = new Order("Email@site.com");
            TestDelegate testDelegate = () => order.SetAsPaid();
            Assert.Throws<InvalidOperationException>(testDelegate);
        }
        
        
        [Test]
        public void order_set_finished_with_no_payment()
        {
            var order = new Order("Email@site.com");
            TestDelegate testDelegate = () => order.SetAsFinished();
            Assert.Throws<InvalidOperationException>(testDelegate);
        }

    }
}