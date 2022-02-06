using System;
using CustomerApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Data.Database
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customer {get;set;}
        public CustomerContext(DbContextOptions<CustomerContext> options) 
        : base(options)
        {
             var customers = new[]
            {
               new Customer
               {
                   Id = Guid.Parse("7f35b48d-cb87-3783-bfdb-21e36012930a"),
                   FirstName = "Sam",
                   LastName = "Kwame",
                   Birthday = new DateTime(1990, 08, 25),
                   Age = 30
               },
               new Customer
               {
                   Id = Guid.Parse("554b7573-9501-436a-ad36-94c5696ac28f"),
                   FirstName = "Paul",
                   LastName = "Atreides",
                   Birthday = new DateTime(3037, 05, 25),
                   Age = 43
               },
               new Customer
               {
                   Id = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1066"),
                   FirstName = "Sasuke",
                   LastName = "Uchiha",
                   Birthday = new DateTime(1988, 02, 16),
                   Age = 83
               }
            };

            Customer.AddRange(customers);
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();
            });
        }

        
    }
}