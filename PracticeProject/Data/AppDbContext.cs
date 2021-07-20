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
    }
}
