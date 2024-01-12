using LanguageExt.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiectpssc.DataTypes
{
    public record CalculatedProduct(ClientID clientID, Quantity quantity, Price price, Price totalPrice)
    {
        public int ProductId { get; set; }
        public bool IsUpdated { get; set; }
        public ClientID ClientId { get; internal set; }
        public Quantity Quantity { get; internal set; }
        public Price Price { get; internal set; }
    }
}
