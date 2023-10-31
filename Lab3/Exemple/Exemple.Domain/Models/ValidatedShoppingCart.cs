using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain.Models
{
    public record ValidatedShoppingCart(ProductCode ProductCode, Product Quantity, Address Address, Product Price);
}
