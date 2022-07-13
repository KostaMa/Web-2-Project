using Microsoft.EntityFrameworkCore;
using ProjectOther.Common.Models;

namespace ProjectOther.DataAccess
{
    public class DataAccessContext : DbContext
    {
        public DataAccessContext()
        { }
        public DataAccessContext(DbContextOptions<DataAccessContext> options) :
            base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Order>()
                .Property(x => x.Id).IsRequired();
            modelBuilder.Entity<Order>()
                .Property(x => x.Address).HasMaxLength(200);
            modelBuilder.Entity<Order>()
                .Property(x => x.Comment).HasMaxLength(2000);

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>()
                .Property(x => x.Id).IsRequired();
            modelBuilder.Entity<Product>()
                .Property(x => x.Name).HasMaxLength(100);
            modelBuilder.Entity<Product>()
                .Property(x => x.Ingredients).HasMaxLength(2000);

            modelBuilder.Entity<OrderProduct>().ToTable("OrderProducts");
            modelBuilder.Entity<Product>()
                .Property(x => x.Id).IsRequired();
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Web2-OtherDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        // }
    }
}
