using Exemple.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Exemple.Domain.Models.ProductValues;

namespace Exemple.Domain
{
    public static class ProductValuesOperation
    {
        public static IProductValues ValidateProductValues(Func<ProductCode, bool> checkProductExists, UnvalidatedProductValues productValues)
        {
            List<ValidatedShoppingCart> validatedProducts = new();
            bool isValidList = true;
            string invalidReson = string.Empty;
            foreach(var unvalidatedProduct in productValues.ValueList)
            {
                if (!Product.TryParseGrade(unvalidatedProduct.Quantity, out Product quantity))
                {
                    invalidReson = $"Invalid quantity ({unvalidatedProduct.ProductCode}, {unvalidatedProduct.Quantity})";
                    isValidList = false;
                    break;
                }
                if (!Address.TryParse(unvalidatedProduct.Address, out Address address))
                {
                    invalidReson = $"Invalid address ({unvalidatedProduct.ProductCode}, {unvalidatedProduct.Address})";
                    isValidList = false;
                    break;
                }

                if (!Product.TryParseGrade(unvalidatedProduct.Price, out Product price))
                {
                    invalidReson = $"Invalid address ({unvalidatedProduct.Price}, {unvalidatedProduct.Price})";
                    isValidList = false;
                    break;
                }

                if (!ProductCode.TryParse(unvalidatedProduct.ProductCode, out ProductCode productCode)
                    && checkProductExists(productCode))
                {
                    invalidReson = $"Invalid product code ({unvalidatedProduct.ProductCode})";
                    isValidList = false;
                    break;
                }
                ValidatedShoppingCart validGrade = new(productCode, quantity,address,price);
                validatedProducts.Add(validGrade);
            }

            if (isValidList)
            {
                return new ValidatedProductValues(validatedProducts);
            }
            else
            {
                return new InvalidatedProductValues(productValues.ValueList, invalidReson);
            }

        }

        public static IProductValues CalculateFinalPrice(IProductValues productValues) => productValues.Match(
            whenUnvalidatedProductValues: unvalidaTedPrice => unvalidaTedPrice,
            whenInvalidatedProductValues: invalidPrice => invalidPrice,
            whenCalculatedPrice: calculatedPrice => calculatedPrice,
            whenPublishedProductValues: publishedPrice => publishedPrice,
            whenValidatedProductValues: validProductValues =>
            {
                var calculatedPrice = validProductValues.ValueList.Select(validProd =>
                                            new CalculatedShoppingCart(validProd.ProductCode,
                                                                      validProd.Quantity,
                                                                      validProd.Price,
                                                                      validProd.Address,
                                                                      validProd.Quantity * validProd.Price));
                return new CalculatedPrice(calculatedPrice.ToList().AsReadOnly());
            }
        );

        public static IProductValues PublishProductValues(IProductValues productValues) => productValues.Match(
            whenUnvalidatedProductValues: unvalidaTedExam => unvalidaTedExam,
            whenInvalidatedProductValues: invalidExam => invalidExam,
            whenValidatedProductValues: validatedExam => validatedExam,
            whenPublishedProductValues: publishedExam => publishedExam,
            whenCalculatedPrice: calculatedPrice =>
            {
                StringBuilder csv = new();
                calculatedPrice.ValueList.Aggregate(csv, (export, product) => export.AppendLine($"{product.ProductCode.Value}, {product.Quantity}, {product.Address.Value} , {product.Price}, {product.FinalPrice}"));

                PublishedProductValues publishedProductValues = new(calculatedPrice.ValueList, csv.ToString(), DateTime.Now);

                return publishedProductValues;
            });
    }
}
