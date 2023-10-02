using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMovies.Entities
{
    public class PaypalPayment: Payment
    {
        public string EmailAddress { get; set; }
    }
}