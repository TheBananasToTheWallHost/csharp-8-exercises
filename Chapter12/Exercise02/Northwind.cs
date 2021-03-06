using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    // this manages the connection to the database
    public class Northwind : DbContext
    {
        // these properties map to tables in the database 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers {get; set;}

        protected override void OnConfiguring(
          DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Northwind.db");

            optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(
          ModelBuilder modelBuilder)
        {
            // example of using Fluent API instead of attributes
            // to limit the length of a category name to under 15
            modelBuilder.Entity<Category>()
              .Property(category => category.CategoryName)
              .IsRequired() // NOT NULL
              .HasMaxLength(15);

            modelBuilder.Entity<Customer>()
              .Property(customer => customer.CustomerID)
              .IsRequired()
              .HasMaxLength(5)
              .IsFixedLength();
            
            modelBuilder.Entity<Customer>()
              .Property(customer => customer.CustomerID)
              .IsRequired()
              .HasMaxLength(40);

            modelBuilder.Entity<Customer>()
              .Property(customer => customer.CustomerID)
              .HasMaxLength(15);
              
        }
    }
}