using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using proiectpssc.Exceptions;

namespace proiectpssc.DataTypes
{
    public record ProductID
    {
        private static readonly Regex ValidIDPattern = new("^PID[0-9]{3}$");
        public string Value { get; }

        internal ProductID(string value)
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

        public static Option<ProductID> TryParse(string stringValue)
        {
            if (IsValid(stringValue))
            {
                return Some(new ProductID(stringValue));
            }
            else
            {
                return None;
            }
        }

        private static bool IsValid(string stringValue) => ValidIDPattern.IsMatch(stringValue);
    }
}
