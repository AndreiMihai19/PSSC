using LanguageExt;
using proiectpssc.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiectpssc.Repositories
{
    public interface IInvoiceRepository
    {
        TryAsync<List<InvoiceID>> TryGetExistingInvoices(IEnumerable<string> invoiceToCheck);
    }
}
