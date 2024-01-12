using proiectpssc.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiectpssc.Commands
{
    public record PaymentCartCommand
    {
        public PaymentCartCommand(IReadOnlyCollection<UnvalidatedProduct> inputProducts)
        {
            InputProducts = inputProducts;
        }

        public IReadOnlyCollection<UnvalidatedProduct> InputProducts { get; }
    }
}
