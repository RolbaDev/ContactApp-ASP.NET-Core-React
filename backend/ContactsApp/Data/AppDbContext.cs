using ContactsApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;

namespace ContactsApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        // Configure model relationships and properties     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>()
                .Property(c => c.SubcategoryId)
                .IsRequired(false);

            // Define the relationship between Contact and Category
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define the relationship between Contact and Subcategory
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Subcategory)
                .WithMany()
                .HasForeignKey(c => c.SubcategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}