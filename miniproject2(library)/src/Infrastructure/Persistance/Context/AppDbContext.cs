using Microsoft.EntityFrameworkCore;
using Onion.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Context
{
    public   class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("server=localhost;database=Library;trusted_connection=true;TrustServerCertificate=true");
        }
        public DbSet<Book>Books { get; set; }
        public DbSet<Author>Authors { get; set; }
        public DbSet<ReservedItem> ReservedItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReservedItem>()
                .Property(r => r.FinCode)
                .HasColumnType("NCHAR(7)");
        }
    }
}
