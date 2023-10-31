using Exemple.Domain.Models;
using static Exemple.Domain.Models.ProductValuesPublishedEvent;
using static Exemple.Domain.ProductValuesOperation;
using System;
using static Exemple.Domain.Models.ProductValues;

namespace Exemple.Domain
{
    public class PublishProductWorkflow
    {
        public IProductValuesPublishedEvent Execute(PublishValuesCommand command, Func<ProductCode, bool> checkProductExists)
        {
            UnvalidatedProductValues unvalidatedProducts = new UnvalidatedProductValues(command.InputProductValues);
            IProductValues price = ValidateProductValues(checkProductExists, unvalidatedProducts);
            price = CalculateFinalPrice(price);
            price = PublishProductValues(price);

            return price.Match(
                    whenUnvalidatedProductValues: unvalidatedGrades => new ProductValuesPublishFaildEvent("Unexpected unvalidated state") as IProductValuesPublishedEvent,
                    whenInvalidatedProductValues: invalidGrades => new ProductValuesPublishFaildEvent(invalidGrades.Reason),
                    whenValidatedProductValues: validatedGrades => new ProductValuesPublishFaildEvent("Unexpected validated state"),
                    whenCalculatedPrice: calculatedGrades => new ProductValuesPublishFaildEvent("Unexpected calculated state"),
                    whenPublishedProductValues: publishedProducts => new ProductValuesPublishSucceededEvent(publishedProducts.Csv, publishedProducts.PublishedDate)
                );
        }
    }
}
