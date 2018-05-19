using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using jhray.com.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace jhray.com.Database
{
    public class ChilledDbContext : IdentityDbContext<ChilledUser>
    {
        public ChilledDbContext() { }

        public ChilledDbContext(DbContextOptions<ChilledDbContext> options) :base(options)
        {

        }

        public DbSet<Gem> Gems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // modify user here because IdentityDbContext sets stuff up for us first
            builder.Entity<ChilledUser>().ToTable("ChilledUser");
            builder.Entity<ChilledUser>().HasMany(u => u.CreatedGems).WithOne(g => g.CreatedBy);
            // shadow properties
            //builder.Entity<User>().Property<DateTimeOffset>("UpdatedTimestamp");
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            //updateUpdatedProperty<ChilledUser>();

            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            //update tracked properties here

        }
    }
}
