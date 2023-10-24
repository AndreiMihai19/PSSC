using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Entities
{
    internal class Order
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public Address DeliveryAddress { get; set; }
    }
}
