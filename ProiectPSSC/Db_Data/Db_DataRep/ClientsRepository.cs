using LanguageExt;
using proiectpssc.DataTypes;
using proiectpssc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace proiectpssc.Db_Data.Db_DataRep
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly ProductsContext productsContext;

        public ClientsRepository(ProductsContext productsContext)
        {
            this.productsContext = productsContext;
        }

        public TryAsync<List<ClientID>> TryGetExistingClients(IEnumerable<string> clientsToCheck) => async () =>
        {
            var clients = await productsContext.Clients
                                              .Where(client => clientsToCheck.Contains(client.ClientCode))
                                              .AsNoTracking()
                                              .ToListAsync();
            return clients.Select(client => new ClientID(client.ClientCode))
                           .ToList();
        };
    }
}
