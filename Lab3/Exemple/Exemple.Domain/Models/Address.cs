using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Exemple.Domain.Models
{
    public record Address
    {
        private static readonly Regex ValidPattern = new("^STR[0-9]{3}$");

        public string Value { get; }

        private Address(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidAddressException("");
            }
        }

        private static bool IsValid(string stringValue) => ValidPattern.IsMatch(stringValue);

        public override string ToString()
        {
            return Value;
        }

        public static bool TryParse(string stringValue, out Address address)
        {
            bool isValid = false;
            address = null;

            if (IsValid(stringValue))
            {
                isValid = true;
                address = new(stringValue);
            }

            return isValid;
        }
    }
}
