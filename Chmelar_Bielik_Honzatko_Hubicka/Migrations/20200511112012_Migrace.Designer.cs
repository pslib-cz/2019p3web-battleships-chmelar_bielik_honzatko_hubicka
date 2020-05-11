﻿// <auto-generated />
using System;
using Chmelar_Bielik_Honzatko_Hubicka.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chmelar_Bielik_Honzatko_Hubicka.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200511112012_Migrace")]
    partial class Migrace
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("Chmelar_Bielik_Honzatko_Hubicka.Models.Game", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentPlayerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gamestate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OwnerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("OwnerState")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Player1Board")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Player2Board")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlayerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayerState")
                        .HasColumnType("INTEGER");

                    b.HasKey("GameId");

                    b.HasIndex("CurrentPlayerId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Chmelar_Bielik_Honzatko_Hubicka.Models.NavyBattlePiece", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BoardId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<int>("PosX")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PosY")
                        .HasColumnType("INTEGER");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("NavyBattlePieces");
                });

            modelBuilder.Entity("Chmelar_Bielik_Honzatko_Hubicka.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerState")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT")
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Chmelar_Bielik_Honzatko_Hubicka.Models.Game", b =>
                {
                    b.HasOne("Chmelar_Bielik_Honzatko_Hubicka.Models.User", "CurrentPlayer")
                        .WithMany("GamesPlay")
                        .HasForeignKey("CurrentPlayerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Chmelar_Bielik_Honzatko_Hubicka.Models.User", "Owner")
                        .WithMany("GamesOwner")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Chmelar_Bielik_Honzatko_Hubicka.Models.User", "Player")
                        .WithMany("GamesPlayer")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Chmelar_Bielik_Honzatko_Hubicka.Models.NavyBattlePiece", b =>
                {
                    b.HasOne("Chmelar_Bielik_Honzatko_Hubicka.Models.Game", "Game")
                        .WithMany("GamePieces")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chmelar_Bielik_Honzatko_Hubicka.Models.User", "User")
                        .WithMany("Pieces")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Chmelar_Bielik_Honzatko_Hubicka.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Chmelar_Bielik_Honzatko_Hubicka.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chmelar_Bielik_Honzatko_Hubicka.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Chmelar_Bielik_Honzatko_Hubicka.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}