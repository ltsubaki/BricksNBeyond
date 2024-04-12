using Microsoft.EntityFrameworkCore;

namespace IntexQueensSlay.Models
{
    public partial class LegoContext : DbContext
    {
        public LegoContext()
        {
        }

        public LegoContext(DbContextOptions<LegoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }

        public virtual DbSet<LineItems> LineItems { get; set; }

        public virtual DbSet<Orders> Orders { get; set; }

        public virtual DbSet<Products> Products { get; set; }
    }
}
