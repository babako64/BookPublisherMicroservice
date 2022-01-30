using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {

                orderContext.Database.Migrate();

                if (!orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreconfiguredOrders());
                    await orderContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                if (retryForAvailability < 3)
                {
                    retryForAvailability++;
                    await SeedAsync(orderContext, retryForAvailability);
                }
            }

        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
            };
        }
    }
}
