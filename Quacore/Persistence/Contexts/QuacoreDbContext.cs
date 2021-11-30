using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Quacore.Domain.Models;

namespace Quacore.Persistence.Contexts
{
    public class QuacoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Quack> Quacks { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Mention> Mentions { get; set; }
        public DbSet<BaseToken> BaseTokens { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        public QuacoreDbContext() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region User
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(128);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(64);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(128);

            modelBuilder.Entity<User>()
                .HasMany(x => x.RefreshTokens)
                .WithOne(y => y.User)
                .HasForeignKey(x => x.UserId);
            #endregion

            #region Quack
            modelBuilder.Entity<Quack>()
                .Property(q => q.Content)
                .IsRequired()
                .HasMaxLength(420);

            modelBuilder.Entity<Quack>()
                .Property(q => q.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Quack>()
                .HasOne(q => q.User)
                .WithMany(u => u.Quacks);
            #endregion

            #region Profile
            modelBuilder.Entity<Profile>()
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(420);

            modelBuilder.Entity<Profile>()
                .Property(p => p.ImageLink)
                .IsRequired()
                .HasMaxLength(280);

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Mention
            modelBuilder.Entity<Mention>()
                .HasOne(m => m.User)
                .WithMany(u => u.Mentions);

            modelBuilder.Entity<Mention>()
                .HasOne(m => m.Quack)
                .WithMany(q => q.Mentions);
            #endregion

            #region Follow
            modelBuilder.Entity<Follow>()
                .HasKey(f => new { f.FollowedId, f.FollowerId });

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Followed)
                .WithMany(u => u.Followees)
                .HasForeignKey(f => f.FollowedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.FollowerId);
            #endregion

            #region Token
            modelBuilder.Entity<BaseToken>()
                .ToTable("Tokens");
            
            modelBuilder.Entity<BaseToken>()
                .Property(t => t.Token)
                .IsRequired();

            modelBuilder.Entity<BaseToken>()
                .Property(t => t.Expiration)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>()
                .HasOne(x => x.User)
                .WithMany(y => y.RefreshTokens)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<BaseToken>()
                .HasDiscriminator(t => t.Discriminator);

            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("QuacoreConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }    
        }
    }
}
