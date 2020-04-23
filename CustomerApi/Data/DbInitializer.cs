using System.Collections.Generic;
using System.Linq;
using CustomerApi.Models;

namespace CustomerApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(CustomerApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Customers
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            List<Customer> customers = new List<Customer>
            {
                new Customer
                {
                    Name = "John Smith",
                    Email = "john@mail.com",
                    PhoneNumber = "+45525487",
                    BillingAddress = "John Street 04",
                    ShippingAddress = "John Street 04",
                    CreditStanding = true
                },
                new Customer
                {
                    Name = "Bames Nond",
                    Email = "nond@mail.com",
                    PhoneNumber = "+45585487",
                    BillingAddress = "Bondulance Street 33",
                    ShippingAddress = "Stronk Street 45",
                    CreditStanding = true
                },
                new Customer
                {
                    Name = "James Name",
                    Email = "name@mail.com",
                    PhoneNumber = "+45585466",
                    BillingAddress = "Bondulance Street 04",
                    ShippingAddress = "Stronk Street 04",
                    CreditStanding = false
                },
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
