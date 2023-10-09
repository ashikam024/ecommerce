using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceProject1.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string AccountHolder { get; set; }
        public string RoutingNumber { get; set; }
    }
}