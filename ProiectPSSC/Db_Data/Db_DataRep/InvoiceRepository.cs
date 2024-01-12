using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using proiectpssc.DataTypes;
using proiectpssc.Repositories;

namespace proiectpssc.Db_Data.Db_DataRep
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceContext invoiceContext;

        public InvoiceRepository(InvoiceContext invoiceContext)
        {
            this.invoiceContext = invoiceContext;
        }

        public TryAsync<List<InvoiceID>> TryGetExistingInvoices(IEnumerable<string> invoicesToCheck) => async () =>
        {
            var invoices = await invoiceContext.Invoices
                                              .Where(invoice => invoicesToCheck.Contains(invoice.InvoiceNumber))
                                              .AsNoTracking()
                                              .ToListAsync();
            return invoices.Select(invoice => new InvoiceID(invoice.InvoiceNumber))
                           .ToList();
        };
    }
}
