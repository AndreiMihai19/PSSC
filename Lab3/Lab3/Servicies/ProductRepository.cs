using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab3.Entities;

namespace Lab3.Servicies
{
    internal class ProductRepository
    {
        private List<Product> products = new List<Product>();

        public Product GetProductByCode(string productCode)
        {
            return products.Find(p => p.ProductCode == productCode);
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }
    }
}
