using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiectpssc.DataTypes;

namespace proiectpssc.Commands
{
    public record InvoiceCommand
    {
        public InvoiceCommand(IReadOnlyCollection<ValidatedProduct> validatedProducts, Client client)
        {
            ValidatedProducts = validatedProducts ?? throw new ArgumentNullException(nameof(validatedProducts));
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public IReadOnlyCollection<ValidatedProduct> ValidatedProducts { get; }
        public Client Client { get; }
    }
}
