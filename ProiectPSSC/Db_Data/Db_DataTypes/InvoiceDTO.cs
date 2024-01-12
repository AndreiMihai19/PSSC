using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proiectpssc.DataTypes;

namespace proiectpssc.Db_Data.Db_DataTypes
{
    public class InvoiceDTO
    {
        public int InvoiceID { get; set; }
        public Client Client { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
