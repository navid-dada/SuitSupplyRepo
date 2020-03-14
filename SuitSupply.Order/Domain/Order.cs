using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SuitSupply.DataAccess.Model;
using SuitSupply.Messages;
using SuitSupply.Messages.Commands;


namespace SuitSupply.Order.Domain
{
    public class Order: BaseEntity
    {
        public OrderState State { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime? PaidDatetime { get; private set; }
        public DateTime? FinisedDateTime { get; private set; }
        private List<Alteration> _alterations;
        public string CustomerEmail { get; set; }

        private 
        protected Order()
        {
            _alterations = new List<Alteration>();
            State = OrderState.Registered;
        }

        public Order(string customerEmail)
        {
            _alterations = new List<Alteration>();
            State = OrderState.Registered;
            CustomerEmail = customerEmail;
            CreatedDateTime = DateTime.Now;
            PaidDatetime = null;
            FinisedDateTime = null;
        }

        public IEnumerable<Alteration> Alterations => _alterations;

        public void AddAlteration(Alteration Alternation)
        {
            var item = _alterations.FirstOrDefault(x => x.AlterationPart == Alternation.AlterationPart && x.AlterationSide == Alternation.AlterationSide);
            if (item == null)
            {
                _alterations.Add(Alternation);
            }
            else
            {
                _alterations.Remove(item);
                _alterations.Add(Alternation);
            }
        }

        public void SetAsPaid()
        {
            if (!this._alterations.Any())
            {
                throw new InvalidOperationException("Alternations is Empty");
            }

            this.State = OrderState.Paid;
            this.PaidDatetime = DateTime.Now;
        }

        public void SetAsFinished()
        {
            if (this.State != OrderState.Paid)
            {
                throw new InvalidOperationException("cannot finish order without payment");
            }

            this.State = OrderState.Finished;
            this.FinisedDateTime = DateTime.Now;
        }

        
    }

    public class Alteration: BaseEntity
    {
        public AlterationPart AlterationPart { get; private set; }
        public AlterationSide AlterationSide { get; private set; }
        public AlterationType AlterationType { get; private set;}
        
        public float AlterationLength { get; private set; }

        protected Alteration()
        {
        }

        protected Alteration(float lenght)
        {
            AlterationLength = lenght;
        }

        public static Alteration CreateSleeveAlterationInstance(float lenght, AlterationSide side, AlterationType type= AlterationType.Increscent)
        {
            if (lenght < 0 || lenght > 5)
            {
                throw new InvalidOperationException("you can increase or decrease 5cm only");
            }

            var Alternation = new Alteration(Math.Abs(lenght));
            Alternation.AlterationPart = AlterationPart.Sleeves;
            Alternation.AlterationSide = side;
            Alternation.AlterationType = type;
            return Alternation;
        }
        
        public static Alteration CreateTrousersAlterationInstance(float lenght, AlterationSide side, AlterationType type= AlterationType.Increscent)
        {
            if (lenght < 0 || lenght > 5)
            {
                throw new InvalidOperationException("you can increase or decrease 5cm only");
            }

            var Alternation = new Alteration(Math.Abs(lenght));
            Alternation.AlterationPart = AlterationPart.Trousers;
            Alternation.AlterationSide = side;
            Alternation.AlterationType = type;
            return Alternation;
        }
    }
    
}