using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain.Models
{
    public record CalculatedShoppingCart(ProductCode ProductCode, Product Quantity, Product Price, Address Address, Product FinalPrice);
}
