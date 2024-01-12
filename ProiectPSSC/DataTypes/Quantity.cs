using static LanguageExt.Prelude;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiectpssc.Exceptions;

namespace proiectpssc.DataTypes
{
    public record Quantity
    {
        public decimal Value { get; }
        internal Quantity(decimal value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidQuantityException($"{Value} is an invalid quantity");
            }
        }
        public static Quantity operator *(Quantity a, Quantity b) => new Quantity((a.Value * b.Value));
        public override string ToString()
        {
            return $"{Value:0}";
        }
        public static Option<Quantity> TryParseQuantity(string quantityString)
        {
            if (decimal.TryParse(quantityString, out decimal numericQuantity) && IsValid(numericQuantity))
            {
                return Some<Quantity>(new Quantity(numericQuantity));
            }
            else
            {
                return None;
            }
        }
        public static bool IsValid(decimal numericQuantity) => numericQuantity > 0;
    }
}
