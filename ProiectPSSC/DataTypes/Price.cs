﻿using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using proiectpssc.Exceptions;

namespace proiectpssc.DataTypes
{
    public record Price
    {
        public decimal Value { get; }

        internal Price(decimal value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidPriceException($"{Value} is an invalid price");
            }
        }

        public static Price operator *(Price a, Quantity b) => new Price((a.Value * b.Value));

        public override string ToString()
        {
            return $"{Value:0.##}";
        }

        public static Option<Price> TryParsePrice(string priceString)
        {
            if (decimal.TryParse(priceString, out decimal numericPrice) && IsValid(numericPrice))
            {
                return Some<Price>(new Price(numericPrice));
            }
            else
            {
                return None;
            }
        }

        public static bool IsValid(decimal numericPrice) => numericPrice > 0;
        public static Price FromDecimal(decimal value) => new Price(value);
        public static Price Zero => new Price(0);

        public Price Add(Price other) => new Price(Value + other.Value);
    }
}
