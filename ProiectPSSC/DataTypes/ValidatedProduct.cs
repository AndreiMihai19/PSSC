using LanguageExt.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiectpssc.DataTypes
{
    public record ValidatedProduct(ClientID clientID, Quantity quantity, Price price);
}
