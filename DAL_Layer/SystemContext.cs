using BO;
using BO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL_Layer
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions opts) : base(opts)
        {
        }

        // Add Model Here
        public DbSet<User> User { get; set; }

        public DbSet<PhoneRanking> PhoneRanking { get; set; }

        public DbSet<PhoneRankingHistory> PhoneRankingHistorie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add Index
            modelBuilder.Entity<User>()
                .HasIndex(o => new { o.UserName, o.Email });
            modelBuilder.Entity<PhoneRanking>()
               .HasIndex(o => new { o.Ranking, o.DeviceName, o.OS, o.Ram, o.StorageSize });
            modelBuilder.Entity<PhoneRankingHistory>()
               .HasIndex(o => new { o.Ranking, o.DeviceName, o.OS, o.Ram, o.StorageSize, o.RankingDate });
        }

        public Func<DateTime> TimestampProvider { get; set; } = () => DateTime.Now;

        public override int SaveChanges()
        {
            TrackChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            TrackChanges();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void TrackChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is IAuditable)
                {
                    IAuditable auditable = entry.Entity as IAuditable;
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedDate = TimestampProvider();

                        auditable.UpdateDate = TimestampProvider();
                    }
                    else
                    {
                        auditable.UpdateDate = TimestampProvider();
                    }
                }
            }
        }
    }
}