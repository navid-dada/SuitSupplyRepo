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
        private List<Alternation> _alternations;
        public string CustomerEmail { get; set; }

        private 
        protected Order()
        {
            _alternations = new List<Alternation>();
            State = OrderState.Registered;
        }

        public Order(string customerEmail)
        {
            _alternations = new List<Alternation>();
            State = OrderState.Registered;
            CustomerEmail = customerEmail;
            CreatedDateTime = DateTime.Now;
            PaidDatetime = null;
            FinisedDateTime = null;
        }

        public IEnumerable<Alternation> Alternations => _alternations;

        public void AddAlternation(Alternation alternation)
        {
            var item = _alternations.FirstOrDefault(x => x.AlternationPart == alternation.AlternationPart && x.AlternationSide == alternation.AlternationSide);
            if (item == null)
            {
                _alternations.Add(alternation);
            }
            else
            {
                _alternations.Remove(item);
                _alternations.Add(alternation);
            }
        }

        public void SetAsPaid()
        {
            if (!this._alternations.Any())
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

    public class Alternation: BaseEntity
    {
        public AlternationPart AlternationPart { get; private set; }
        public AlternationSide AlternationSide { get; private set; }
        public AlternationType AlternationType { get; private set;}
        
        public float AlternationLength { get; private set; }

        protected Alternation()
        {
        }

        protected Alternation(float lenght)
        {
            AlternationLength = lenght;
        }

        public static Alternation CreateSleeveAlternationInstance(float lenght, AlternationSide side, AlternationType type= AlternationType.Increscent)
        {
            if (lenght < 0 || lenght > 5)
            {
                throw new InvalidOperationException("you can increase or decrease 5cm only");
            }

            var alternation = new Alternation(Math.Abs(lenght));
            alternation.AlternationPart = AlternationPart.Sleeves;
            alternation.AlternationSide = side;
            alternation.AlternationType = type;
            return alternation;
        }
        
        public static Alternation CreateTrousersAlternationInstance(float lenght, AlternationSide side, AlternationType type= AlternationType.Increscent)
        {
            if (lenght < 0 || lenght > 5)
            {
                throw new InvalidOperationException("you can increase or decrease 5cm only");
            }

            var alternation = new Alternation(Math.Abs(lenght));
            alternation.AlternationPart = AlternationPart.Trousers;
            alternation.AlternationSide = side;
            alternation.AlternationType = type;
            return alternation;
        }
    }
    
}