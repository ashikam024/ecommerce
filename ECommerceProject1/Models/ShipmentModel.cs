﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceProject1.Models
{
    public class ShipmentModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public  Payment PaymentType { get; set; }
    }
}