using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain.Models
{
    public record UnvalidatedShoppingCart(string ProductCode, string Quantity, string Address, string Price);
}
