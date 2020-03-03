using System;
using System.Collections.Generic;

namespace DTOs
{
    public enum Status
    {
        Completed,
        Canceled,
        Shipped,
        Paid
    }

    public class OrderDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? Date { get; set; }
        public Status Status { get; set; }

        public List<OrderLineDTO> OrderLines { get; set; }
    }
}
