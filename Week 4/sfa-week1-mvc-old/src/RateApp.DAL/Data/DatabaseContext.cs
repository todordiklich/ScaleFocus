using Microsoft.EntityFrameworkCore;
using RateApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateApp.DAL.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>().HasMany(r => r.Owners).WithMany("Restaurants");
            modelBuilder.Entity<Restaurant>().Property(r=>r.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<User>().Property(r => r.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Review>().Property(r => r.CreatedAt).HasDefaultValueSql("getdate()");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer("Server=.;Database=RateApp;Trusted_Connection=True;");

        }
    }
}
