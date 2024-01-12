using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiectpssc.DataTypes
{
    public record UnvalidatedProduct(string clientId, string quantity, string price);
}
