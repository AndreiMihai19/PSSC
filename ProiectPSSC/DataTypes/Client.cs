using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiectpssc.DataTypes
{
    public class Client
    {
        public Client(string name, string address)
        {
            ClientId = Guid.NewGuid();
            Name = name;
            Address = address;

        }
        public Guid ClientId { get; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
