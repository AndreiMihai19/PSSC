using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LanguageExt;
using proiectpssc.Exceptions;

namespace proiectpssc.DataTypes
{
    public record InvoiceID
    {
        private static readonly Regex ValidPattern = new("^O[0-9]{3}$");
        public string Value { get; }
        internal InvoiceID(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidProductIDException($"{Value} is an invalid product code");
            }
        }
        public override string ToString()
        {
            return Value;
        }
        public static Option<InvoiceID> TryParse(string stringValue)
        {
            if (IsValid(stringValue))
            {
                return Some<InvoiceID>(new InvoiceID(stringValue));
            }
            else
            {
                return None;
            }
        }
        private static bool IsValid(string stringValue) => ValidPattern.IsMatch(stringValue);
    }
}
