using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Exemple.Domain.Models.ProductValues;

namespace Exemple.Domain.Models
{
    public record PublishValuesCommand
    {
        public PublishValuesCommand(IReadOnlyCollection<UnvalidatedShoppingCart> inputProductValues)
        {
            InputProductValues = inputProductValues;
        }

        public IReadOnlyCollection<UnvalidatedShoppingCart> InputProductValues { get; }
    }
}
