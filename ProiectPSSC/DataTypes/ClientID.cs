using static LanguageExt.Prelude;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSharp.Choices;
using LanguageExt;
using proiectpssc.Exceptions;

namespace proiectpssc.DataTypes
{
    public record ClientID
    {
        private static readonly Regex ValidPattern = new("^ID[0-9]{3}$");
        public string Value { get; }
        internal ClientID(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidClientIDException("");
            }
        }
        private static bool IsValid(string stringValue) => ValidPattern.IsMatch(stringValue);

        public override string ToString()
        {
            return Value;
        }

        public static Option<ClientID> TryParse(string stringValue)
        {
            if (IsValid(stringValue))
            {
                return Some<ClientID>(new ClientID(stringValue));
            }
            else
            {
                return None;
            }
        }
    }
}
