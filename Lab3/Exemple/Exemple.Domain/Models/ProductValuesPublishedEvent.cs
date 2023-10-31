using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Domain.Models
{
    [AsChoice]
    public static partial class ProductValuesPublishedEvent
    {
        public interface IProductValuesPublishedEvent { }

        public record ProductValuesPublishSucceededEvent : IProductValuesPublishedEvent 
        {
            public string Csv{ get;}
            public DateTime PublishedDate { get; }

            internal ProductValuesPublishSucceededEvent(string csv, DateTime publishedDate)
            {
                Csv = csv;
                PublishedDate = publishedDate;
            }
        }

        public record ProductValuesPublishFaildEvent : IProductValuesPublishedEvent 
        {
            public string Reason { get; }

            internal ProductValuesPublishFaildEvent(string reason)
            {
                Reason = reason;
            }
        }
    }
}
