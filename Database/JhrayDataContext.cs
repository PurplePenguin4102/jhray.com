using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using jhray.com.Database.Entities;

namespace jhray.com.Database
{
    public class JhrayDataContext : DbContext
    {
        public JhrayDataContext(DbContextOptions<JhrayDataContext> options) :base(options)
        {

        }

        DbSet<User> Users { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<Role> Roles { get; set; }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(m => m.Id);
            builder.Entity<User>().HasIndex(u => u.Id).IsUnique().HasName("User_Idx");
            builder.Entity<User>().HasIndex(u => u.Name).HasName("UserName_Idx");

            builder.Entity<User>().HasMany(u => u.UserRoles).WithOne(ur => ur.User);
            builder.Entity<Role>().HasMany(r => r.UserRoles).WithOne(ur => ur.Role);

            builder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);

            // shadow properties
            builder.Entity<User>().Property<DateTimeOffset>("UpdatedTimestamp");
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<User>();

            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTimeOffset.UtcNow;
            }
        }
    }
}
