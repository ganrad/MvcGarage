using MvcGarage.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcGarage.Data {
    public class RepairContext : DbContext 
    {
            public RepairContext(DbContextOptions<RepairContext> options) : base(options)
            {
            }

            public DbSet<Repair> Repairs { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Repair>().ToTable("Repair");
            }
    }
}