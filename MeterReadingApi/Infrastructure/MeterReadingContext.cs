using MeterReadingApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeterReadingApi.Infrastructure
{
    public class MeterReadingContext  : DbContext
    {
        public MeterReadingContext(DbContextOptions<MeterReadingContext> options) : base(options) { }

        public DbSet<MeterReading> MeterReadings { get; set; }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MeterReading>()
                .HasOne<Account>()
                .WithMany()
                .HasForeignKey(mr => mr.AccountId);

            modelBuilder.Entity<MeterReading>()
                .HasIndex(mr => new { mr.AccountId, mr.MeterReadingDateTime })
                .IsUnique();
        }

    }
}
