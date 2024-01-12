using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static proiectpssc.DataTypes.Cart;

namespace proiectpssc.Events
{
    [AsChoice]
    public static partial class PayCartEvent
    {
        public interface IPayCartEvent { }

        public record PayCartSucceededEvent : IPayCartEvent
        {
            public string Csv { get; }
            public DateTime PaidDate { get; }
            public PaidCart PaidCart { get; }

            internal PayCartSucceededEvent(string csv, DateTime paidDate, PaidCart paidCart)
            {
                Csv = csv;
                PaidDate = paidDate;
                PaidCart = paidCart;
            }

        }

        public record PayCartFailedEvent : IPayCartEvent
        {
            public string Reason { get; }

            internal PayCartFailedEvent(string reason)
            {
                Reason = reason;
            }
        }
    }
}
