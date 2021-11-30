﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quacore.Persistence.Contexts;

namespace Quacore.Migrations
{
    [DbContext(typeof(QuacoreDbContext))]
    partial class QuacoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Quacore.Domain.Models.BaseToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tokens");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseToken");
                });

            modelBuilder.Entity("Quacore.Domain.Models.Follow", b =>
                {
                    b.Property<int>("FollowedId")
                        .HasColumnType("int");

                    b.Property<int>("FollowerId")
                        .HasColumnType("int");

                    b.HasKey("FollowedId", "FollowerId");

                    b.HasIndex("FollowerId");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("Quacore.Domain.Models.Mention", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("QuackId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuackId");

                    b.HasIndex("UserId");

                    b.ToTable("Mentions");
                });

            modelBuilder.Entity("Quacore.Domain.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(420)
                        .HasColumnType("nvarchar(420)");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasMaxLength(280)
                        .HasColumnType("nvarchar(280)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Quacore.Domain.Models.Quack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(420)
                        .HasColumnType("nvarchar(420)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Quacks");
                });

            modelBuilder.Entity("Quacore.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Quacore.Domain.Models.AccessToken", b =>
                {
                    b.HasBaseType("Quacore.Domain.Models.BaseToken");

                    b.HasDiscriminator().HasValue("AccessToken");
                });

            modelBuilder.Entity("Quacore.Domain.Models.RefreshToken", b =>
                {
                    b.HasBaseType("Quacore.Domain.Models.BaseToken");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("RefreshToken");
                });

            modelBuilder.Entity("Quacore.Domain.Models.Follow", b =>
                {
                    b.HasOne("Quacore.Domain.Models.User", "Followed")
                        .WithMany("Followees")
                        .HasForeignKey("FollowedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Quacore.Domain.Models.User", "Follower")
                        .WithMany("Followers")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Followed");

                    b.Navigation("Follower");
                });

            modelBuilder.Entity("Quacore.Domain.Models.Mention", b =>
                {
                    b.HasOne("Quacore.Domain.Models.Quack", "Quack")
                        .WithMany("Mentions")
                        .HasForeignKey("QuackId");

                    b.HasOne("Quacore.Domain.Models.User", "User")
                        .WithMany("Mentions")
                        .HasForeignKey("UserId");

                    b.Navigation("Quack");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Quacore.Domain.Models.Profile", b =>
                {
                    b.HasOne("Quacore.Domain.Models.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("Quacore.Domain.Models.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Quacore.Domain.Models.Quack", b =>
                {
                    b.HasOne("Quacore.Domain.Models.User", "User")
                        .WithMany("Quacks")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Quacore.Domain.Models.RefreshToken", b =>
                {
                    b.HasOne("Quacore.Domain.Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Quacore.Domain.Models.Quack", b =>
                {
                    b.Navigation("Mentions");
                });

            modelBuilder.Entity("Quacore.Domain.Models.User", b =>
                {
                    b.Navigation("Followees");

                    b.Navigation("Followers");

                    b.Navigation("Mentions");

                    b.Navigation("Profile");

                    b.Navigation("Quacks");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
