
using Microsoft.EntityFrameworkCore;

namespace IntexQueensSlay.Models

{
    public class LegoContext : DbContext
    {
        public LegoContext(DbContextOptions<LegoContext> options) 
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
