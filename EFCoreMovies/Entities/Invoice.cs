using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMovies.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int InvoiceNumber { get; set; }
    }
}