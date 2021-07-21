using Microsoft.EntityFrameworkCore; // Required to call DbContext base class
using PracticeProject.Models; // Required to call Company model
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // EF configuration to add data to our DB
        {
            
        }

        // Tables to be added to DB
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        // Fluent API from tutorial code...
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Write Fluent API configurations here

            // Property configurations
            modelBuilder.Entity<Company>().Ignore(t => t.Employees); // Tells EF to ignore employees when adding to Company
                                                                     // model (aka like the "[NotMapped]" attribute)

            modelBuilder.Entity<Employee>() // Tells EF that there us a 1-to-many relationship between a company and
                                            // its employees and that the foreign key is CompanyId (aka like the
                                            // "[ForeignKey("CompanyId)]" attribute)
                .HasOne(c => c.Company).WithMany(e => e.Employees).HasForeignKey(c => c.CompanyId);
        }
    }
}
