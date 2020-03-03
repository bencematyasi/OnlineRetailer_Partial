using System;
using System.Collections.Generic;

namespace OrderApi.Models
{
    public enum Status
    {
        Completed,
        Canceled,
        Shipped,
        Paid
    }


    public class Order
    {

        public Order()
        {
            this.OrderLines = new List<OrderLine>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? Date { get; set; }
        public Status Status { get; set; }

        public List<OrderLine> OrderLines { get; set; }
    }
}
