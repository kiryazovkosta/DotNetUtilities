using CounterWebApi.Generators;
using CounterWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CounterWebApi.Data
{
    public class CounterDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; } = null!;

        public CounterDbContext(DbContextOptions options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasSequence<int>("OrdersNumber")
                .StartsAt(1)
                .IncrementsBy(1)
                .IsCyclic(false)
                .HasMax(int.MaxValue);

            modelBuilder.HasSequence<int>("BillOfLandingsNumber")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.Entity<Order>()
                .Property(o => o.Code)
                .HasDefaultValueSql("NEXT VALUE FOR OrdersNumber")
                .HasValueGenerator<SampleCodeGenerator>();
        }
    }
}
