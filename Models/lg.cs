//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

//namespace IntexQueensSlay.Models
//{
//    public partial class LegoContext : IdentityDbContext<IdentityUser, IdentityRole, string>
//    {
//        public LegoContext()
//        {
//        }

//        public LegoContext(DbContextOptions<LegoContext> options)
//            : base(options)
//        {

//        }

//        public virtual DbSet<Customer> Customers { get; set; }

//        public virtual DbSet<LineItem> LineItems { get; set; }

//        public virtual DbSet<Order> Orders { get; set; }

//        public virtual DbSet<Product> Products { get; set; }

//        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        //            => optionsBuilder.UseSqlServer("Data Source=slaygo.db");

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            // Configure primary key for IdentityUserLogin
//            modelBuilder.Entity<IdentityUserLogin<string>>()
//                .HasKey(login => new { login.LoginProvider, login.ProviderKey });

//            modelBuilder.Entity<LineItem>()
//                .HasKey(li => new { li.ProductId, li.TransactionId }); // Define composite primary key for LineItem

//            modelBuilder.Entity<LineItem>()
//                .HasOne(li => li.Product)
//                .WithMany()
//                .HasForeignKey(li => li.ProductId); // Adjust foreign key to only reference ProductId

//            modelBuilder.Entity<LineItem>()
//                .HasOne(li => li.Order)
//                .WithMany()
//                .HasForeignKey(li => li.TransactionId); // Keep TransactionId as foreign key for Order


//            modelBuilder.Entity<Customer>(entity =>
//            {
//                entity.ToTable("Customer");
//                entity.Property(e => e.CustomerId)
//                  .ValueGeneratedNever()
//                  .HasColumnName("customerId");
//                entity.Property(e => e.Age).HasColumnName("age");
//                entity.Property(e => e.BirthDate).HasColumnName("birthDate");
//                entity.Property(e => e.FirstName).HasColumnName("firstName");
//                entity.Property(e => e.Gender).HasColumnName("gender");
//                entity.Property(e => e.LastName).HasColumnName("lastName");
//                entity.Property(e => e.ResCountry).HasColumnName("resCountry");
//            });
//            modelBuilder.Entity<LineItem>(entity =>
//            {
//                entity.HasKey(e => new { e.ProductId, e.TransactionId });
//                entity.ToTable("LineItem");
//                entity.Property(e => e.ProductId).HasColumnName("productId");
//                entity.Property(e => e.TransactionId).HasColumnName("transactionId");
//                entity.Property(e => e.Quantity).HasColumnName("quantity");
//                entity.Property(e => e.Rating).HasColumnName("rating");
//            });
//            modelBuilder.Entity<Order>(entity =>
//            {
//                entity.HasKey(e => e.TransactionId);
//                entity.ToTable("Order");
//                entity.Property(e => e.TransactionId)
//                  .ValueGeneratedNever()
//                  .HasColumnName("transactionId");
//                entity.Property(e => e.Bank).HasColumnName("bank");
//                entity.Property(e => e.CardType).HasColumnName("cardType");
//                entity.Property(e => e.CustomerId).HasColumnName("customerId");
//                entity.Property(e => e.Date).HasColumnName("date");
//                entity.Property(e => e.EntryMode).HasColumnName("entryMode");
//                entity.Property(e => e.Fraud).HasColumnName("fraud");
//                entity.Property(e => e.ShippingAddress).HasColumnName("shippingAddress");
//                entity.Property(e => e.Subtotal).HasColumnName("subtotal");
//                entity.Property(e => e.Time).HasColumnName("time");
//                entity.Property(e => e.TransCountry).HasColumnName("trans_Country");
//                entity.Property(e => e.TransactionType).HasColumnName("transactionType");
//                entity.Property(e => e.WeekDay).HasColumnName("weekDay");
//            });
//            modelBuilder.Entity<Product>(entity =>
//            {
//                entity.ToTable("Product");
//                entity.Property(e => e.ProductId)
//                  .ValueGeneratedNever()
//                  .HasColumnName("productId");
//                entity.Property(e => e.Category1).HasColumnName("category1");
//                entity.Property(e => e.Category2).HasColumnName("category2");
//                entity.Property(e => e.Category3).HasColumnName("category3");
//                entity.Property(e => e.Description).HasColumnName("description");
//                entity.Property(e => e.ImgLink).HasColumnName("imgLink");
//                entity.Property(e => e.Name).HasColumnName("name");
//                entity.Property(e => e.NumParts).HasColumnName("numParts");
//                entity.Property(e => e.Price).HasColumnName("price");
//                entity.Property(e => e.PrimaryColor).HasColumnName("primaryColor");
//                entity.Property(e => e.SecondaryColor).HasColumnName("secondaryColor");
//                entity.Property(e => e.Year).HasColumnName("year");
//            });
//            OnModelCreatingPartial(modelBuilder);
//        }
//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//        //protected override void OnModelCreating(ModelBuilder modelBuilder)
//        //{
            
//        //}







//    }
//}

