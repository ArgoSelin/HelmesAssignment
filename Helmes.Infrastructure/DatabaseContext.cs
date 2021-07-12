using Helmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Helmes.Infrastructure 
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        public DbSet<LetterBag> LetterBag { get; set; }
        public DbSet<ParcelBag> ParcelBag { get; set; }
        public DbSet<FinalBag> FinalBag { get; set; }

        public DbSet<Parcel> Parcel { get; set; }
        public DbSet<Shipment> Shipment { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LetterBag>().HasIndex(u => u.BagNumber).IsUnique();
            builder.Entity<ParcelBag>().HasIndex(u => u.BagNumber).IsUnique();
            builder.Entity<Parcel>().HasIndex(u => u.ParcelNumber).IsUnique();
            builder.Entity<Shipment>().HasIndex(u => u.ShipmentNumber).IsUnique();

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }
    }
}
