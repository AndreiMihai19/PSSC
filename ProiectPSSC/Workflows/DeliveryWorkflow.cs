using Microsoft.Extensions.Logging;
using proiectpssc.Db_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static proiectpssc.DataTypes.Cart;

namespace proiectpssc.Workflows
{
    public class DeliveryWorkflow
    {

            private readonly ProductsContext productsContext;
            private readonly ILogger logger;
            public DeliveryWorkflow(ProductsContext productsContext, ILogger<AddProductsToCartWorkflow> logger)
            {
                this.productsContext = productsContext;
                this.logger = logger;
            }
            public Boolean Validate(int clientID, PaidCart paidCart)
            {
                var result = productsContext.Clients.FirstOrDefault(d => d.ClientId == clientID);
                if (result == null)
                {
                    logger.LogError("Error");
                    return false;
                }
                if (!(result.Address.Length > 0 && result.Address.Length < 50))
                {
                    logger.LogError("Address not valid");
                    return false;
                }
                return true;
            }
        
    }
}
