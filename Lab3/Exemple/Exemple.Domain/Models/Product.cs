using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain.Models
{
    public record Product
    {
        public decimal Value { get; }

        public Product(decimal value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidProductException($"{value:0.##} is an invalid product value.");
            }
        }

        public static Product operator *(Product a, Product b) => new Product(a.Value * b.Value);


        public Product Round()
        {
            var roundedValue = Math.Round(Value);
            return new Product(roundedValue);
        }

        public override string ToString()
        {
            return $"{Value:0.##}";
        }

        public static bool TryParseGrade(string priceString, out Product price)
        {
            bool isValid = false;
            price = null;
            if(decimal.TryParse(priceString, out decimal numericPrice))
            {
                if (IsValid(numericPrice))
                {
                    isValid = true;
                    price = new(numericPrice);
                }
            }

            return isValid;
        }

        private static bool IsValid(decimal numericPrice) => numericPrice > 0 && numericPrice <= 1000;
    }
}
