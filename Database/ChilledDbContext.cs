﻿using System;
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

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<PictureLink> PictureLinks { get; set; }
        public DbSet<RSSHeader> RSSHeaders { get; set; }
        public DbSet<Gem> Gems { get; set; }
        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<PictureTag> PictureTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // modify user here because IdentityDbContext sets stuff up for us first
            builder.Entity<ChilledUser>().ToTable("ChilledUser");
            builder.Entity<ChilledUser>().HasMany(u => u.CreatedGems).WithOne(g => g.CreatedBy);
            builder.Entity<Gem>().HasOne(g => g.PodcastData).WithOne(p => p.GemData).HasForeignKey<Podcast>(p => p.Id);
            builder.Entity<Gem>().HasOne(g => g.PictureData).WithOne(p => p.GemData).HasForeignKey<Picture>(p => p.Id);
            builder.Entity<Picture>().HasMany(pic => pic.PictureTags).WithOne(pt => pt.PictureData);
            builder.Entity<BlogPost>().HasMany(e => e.Pictures).WithOne(p => p.Blog);
            builder.Entity<BlogPost>().HasOne(bp => bp.RSSHeader).WithMany(rss => rss.Blogs);
            builder.Entity<Picture>().HasMany(p => p.BlogLinks).WithOne(p => p.Picture);

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
