﻿using System.ComponentModel;

namespace CustomerApi.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        [DefaultValue(true)]
        public bool CreditStanding { get; set; }
    }
}
