using LanguageExt;
using proiectpssc.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static proiectpssc.DataTypes.Cart;

namespace proiectpssc.Repositories
{
    public interface IProductsRepository
    {
        TryAsync<List<CalculatedProduct>> TryGetExistingProducts();

        TryAsync<Unit> TrySaveProducts(PaidCart paidCart);
    }
}
