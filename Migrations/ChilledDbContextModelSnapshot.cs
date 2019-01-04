﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using jhray.com.Database;

namespace jhray.com.Migrations
{
    [DbContext(typeof(ChilledDbContext))]
    partial class ChilledDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("jhray.com.Database.Entities.BlogPost", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<string>("AuthorId1")
                        .IsRequired();

                    b.Property<string>("Hashtags");

                    b.Property<string>("MarkdownContent");

                    b.Property<DateTime>("Published");

                    b.Property<int>("RSSHeaderId");

                    b.Property<string>("SubTitle");

                    b.Property<string>("Title");

                    b.HasKey("id");

                    b.HasIndex("AuthorId1");

                    b.HasIndex("RSSHeaderId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("jhray.com.Database.Entities.ChilledUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<DateTimeOffset>("Joined");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("ChilledUser");
                });

            modelBuilder.Entity("jhray.com.Database.Entities.Gem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedById");

                    b.Property<string>("FilePath");

                    b.Property<int>("GemType");

                    b.Property<string>("SummaryText");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Gems");
                });

            modelBuilder.Entity("jhray.com.Database.Entities.Picture", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("ArtistLink");

                    b.Property<string>("ArtistName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("FileSize");

                    b.Property<string>("HoverText");

                    b.Property<string>("Location");

                    b.HasKey("Id");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("jhray.com.Database.Entities.PictureLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogId");

                    b.Property<int>("PictureId");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("PictureId");

                    b.ToTable("PictureLinks");
                });

            modelBuilder.Entity("jhray.com.Database.Entities.PictureTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PictureId");

                    b.Property<string>("TagText");

                    b.HasKey("Id");

                    b.HasIndex("PictureId");

                    b.ToTable("PictureTags");
                });

            modelBuilder.Entity("jhray.com.Database.Entities.Podcast", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Description");

                    b.Property<int>("FeedId");

                    b.Property<string>("ItunesDuration");

                    b.Property<long>("LengthInBytes");

                    b.Property<string>("Location");

                    b.Property<DateTimeOffset>("PubDate");

                    b.Property<string>("ShortDescription");

                    b.HasKey("Id");

                    b.ToTable("Podcasts");
                });

            modelBuilder.Entity("jhray.com.Database.Entities.RSSHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AtomLink");

                    b.Property<string>("Author");

                    b.Property<string>("ChannelLink");

                    b.Property<string>("Description");

                    b.Property<string>("ITunesCategory");

                    b.Property<string>("ITunesCategory2");

                    b.Property<string>("ITunesEmail");

                    b.Property<string>("ITunesExplicit");

                    b.Property<string>("ITunesImage");

                    b.Property<string>("ITunesKeywords");

                    b.Property<string>("ITunesName");

                    b.Property<string>("ITunesSubCategory");

                    b.Property<string>("ITunesSubCategory2");

                    b.Property<string>("LastBuildDate");

                    b.Property<string>("LogoLink");

                    b.Property<string>("LogoTitle");

                    b.Property<string>("LogoUrl");

                    b.Property<string>("ManagingEditor");

                    b.Property<string>("PubDate");

                    b.Property<int>("RSSNumber");

                    b.Property<string>("Subtitle");

                    b.Property<string>("Title");

                    b.Property<string>("WebMaster");

                    b.HasKey("Id");

                    b.ToTable("RSSHeaders");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("jhray.com.Database.Entities.ChilledUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("jhray.com.Database.Entities.ChilledUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("jhray.com.Database.Entities.ChilledUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("jhray.com.Database.Entities.ChilledUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jhray.com.Database.Entities.BlogPost", b =>
                {
                    b.HasOne("jhray.com.Database.Entities.ChilledUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId1")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("jhray.com.Database.Entities.RSSHeader", "RSSHeader")
                        .WithMany("Blogs")
                        .HasForeignKey("RSSHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jhray.com.Database.Entities.Gem", b =>
                {
                    b.HasOne("jhray.com.Database.Entities.ChilledUser", "CreatedBy")
                        .WithMany("CreatedGems")
                        .HasForeignKey("CreatedById");
                });

            modelBuilder.Entity("jhray.com.Database.Entities.Picture", b =>
                {
                    b.HasOne("jhray.com.Database.Entities.Gem", "GemData")
                        .WithOne("PictureData")
                        .HasForeignKey("jhray.com.Database.Entities.Picture", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jhray.com.Database.Entities.PictureLink", b =>
                {
                    b.HasOne("jhray.com.Database.Entities.BlogPost", "Blog")
                        .WithMany("Pictures")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("jhray.com.Database.Entities.Picture", "Picture")
                        .WithMany("BlogLinks")
                        .HasForeignKey("PictureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jhray.com.Database.Entities.PictureTag", b =>
                {
                    b.HasOne("jhray.com.Database.Entities.Picture", "PictureData")
                        .WithMany("PictureTags")
                        .HasForeignKey("PictureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jhray.com.Database.Entities.Podcast", b =>
                {
                    b.HasOne("jhray.com.Database.Entities.Gem", "GemData")
                        .WithOne("PodcastData")
                        .HasForeignKey("jhray.com.Database.Entities.Podcast", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
