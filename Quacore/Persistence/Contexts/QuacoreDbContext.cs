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
        public DbSet<Token> Tokens { get; set; }


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
                .Property(u => u.Salt)
                .IsRequired()
                .HasMaxLength(32);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(128);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(128);

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
                .HasMaxLength(420);

            modelBuilder.Entity<Profile>()
                .Property(p => p.BannerImageLink)
                .HasMaxLength(280);
            
            modelBuilder.Entity<Profile>()
                .Property(p => p.AvatarImageLink)
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
            
            modelBuilder.Entity<Token>()
                .Property(t => t.AccessToken)
                .IsRequired();

            modelBuilder.Entity<Token>()
                .Property(t => t.AccessTokenExpiration)
                .IsRequired();

            modelBuilder.Entity<Token>()
                .Property(t => t.RefreshToken)
                .IsRequired();

            modelBuilder.Entity<Token>()
                .Property(t => t.RefreshTokenExpiration)
                .IsRequired();

            modelBuilder.Entity<Token>()
                .HasOne(x => x.User)
                .WithMany(y => y.Tokens);

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
