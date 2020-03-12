using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using SuitSupply.Messages;
using SuitSupply.Order.Domain;
using Alternation = SuitSupply.Order.Domain.Alteration;

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
            order.AddAlteration(Alteration.CreateTrousersAlterationInstance(5, AlterationSide.Left,AlterationType.Decreasement));
            order.AddAlteration(Alteration.CreateTrousersAlterationInstance(finalLenght, AlterationSide.Left, AlterationType.Decreasement));
            Assert.True(order.Alterations.Count() == 1);
            var item = order.Alterations.First();
            Assert.AreEqual(finalLenght, item.AlterationLength);
        }
        
        [Test]
        public void order_add_two_different_side_alt()
        {
            var left_length = 4.5f;
            var right_length = 3.5f;
            var order = new Order("Email@site.com");
            order.AddAlteration(Alteration.CreateTrousersAlterationInstance(left_length, AlterationSide.Left,AlterationType.Decreasement));
            order.AddAlteration(Alteration.CreateTrousersAlterationInstance(right_length, AlterationSide.Right, AlterationType.Increscent));
            Assert.True(order.Alterations.Count() == 2);
            var rightItem = order.Alterations.First(x=>x.AlternationSide== AlterationSide.Right);
            Assert.AreEqual(right_length, rightItem.AlterationLength);
            
            var leftItem = order.Alterations.First(x=>x.AlternationSide== AlterationSide.Right);
            Assert.AreEqual(right_length, leftItem.AlterationLength);
        }

        [Test]
        public void order_add_alt_with_length_greater_than_5()
        {
            var length = 5.1f;
            var order = new Order("Email@site.com");

            void TestDelegate() => order.AddAlteration(Alteration.CreateTrousersAlterationInstance(length, AlterationSide.Left, AlterationType.Decreasement));
            Assert.Throws<InvalidOperationException>(TestDelegate); 
        }
        
        [Test]
        public void order_add_alt_with_length_less_than_0()
        {
            var length = -0.1f;
            var order = new Order("Email@site.com");

            void TestDelegate() => order.AddAlteration(Alteration.CreateTrousersAlterationInstance(length, AlterationSide.Left, AlterationType.Decreasement));
            Assert.Throws<InvalidOperationException>(TestDelegate); 
        }

        [Test]
        public void order_set_paid_with_no_Alternation()
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