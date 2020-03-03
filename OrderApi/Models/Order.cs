using System;
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
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? Date { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public Status Status { get; set; }
    }
}
