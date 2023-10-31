using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain.Models
{
    [AsChoice]
    public static partial class ProductValues
    {
        public interface IProductValues{ }

        public record UnvalidatedProductValues: IProductValues
        {
            public UnvalidatedProductValues(IReadOnlyCollection<Models.UnvalidatedShoppingCart> valueList)
            {
                ValueList = valueList;
            }

            public IReadOnlyCollection<Models.UnvalidatedShoppingCart> ValueList { get; }
        }

        public record InvalidatedProductValues: IProductValues
        {
            internal InvalidatedProductValues(IReadOnlyCollection<Models.UnvalidatedShoppingCart> valueList, string reason)
            {
                ValueList = valueList;
                Reason = reason;
            }

            public IReadOnlyCollection<Models.UnvalidatedShoppingCart> ValueList { get; }
            public string Reason { get; }
        }

        public record ValidatedProductValues: IProductValues
        {
            internal ValidatedProductValues(IReadOnlyCollection<Models.ValidatedShoppingCart> valuesList)
            {
                ValueList = valuesList;
            }

            public IReadOnlyCollection<Models.ValidatedShoppingCart> ValueList { get; }
        }

        public record CalculatedPrice : IProductValues
        {
            internal CalculatedPrice(IReadOnlyCollection<CalculatedShoppingCart> valuesList)
            {
                ValueList = valuesList;
            }

            public IReadOnlyCollection<CalculatedShoppingCart> ValueList { get; }
        }

        public record PublishedProductValues : IProductValues
        {
            internal PublishedProductValues(IReadOnlyCollection<CalculatedShoppingCart> valuesList, string csv, DateTime publishedDate)
            {
                ValueList = valuesList;
                PublishedDate = publishedDate;
                Csv = csv;
            }

            public IReadOnlyCollection<CalculatedShoppingCart> ValueList { get; }
            public DateTime PublishedDate { get; }
            public string Csv { get; }
        }
    }
}
