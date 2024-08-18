using Microsoft.EntityFrameworkCore;
using Referral_System.Models;
using Task = Referral_System.Models.Task;
using Referral_System;


namespace Referral_System
{
    public class ReferralSystemDbContext : DbContext
    {
        public ReferralSystemDbContext(DbContextOptions<ReferralSystemDbContext> options) : base(options) { } 

        public DbSet<User> Users { get; set; }
        public DbSet<Referral> Referrals { get; set; }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReferralsMade)
                .WithOne(r => r.Referrer)
                .HasForeignKey(r => r.ReferrerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReferralsReceived)
                .WithOne(r => r.ReferredUser)
                .HasForeignKey(r => r.ReferredUserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
