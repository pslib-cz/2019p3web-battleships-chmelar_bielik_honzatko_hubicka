using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Game> Games { get; set; }
        public DbSet<NavyBattlePiece> NavyBattlePieces { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Battleships;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Fluent API
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.CurrentPlayer)
                .WithMany(u => u.GamesPlay)
                .HasForeignKey(u => u.CurrentPlayerId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                .HasMany(u => u.GamesPlay)
                .WithOne(g => g.CurrentPlayer);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Owner)
                .WithMany(u => u.GamesOwner)
                .HasForeignKey(g => g.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(u => u.GamesOwner)
                .WithOne(g => g.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Player)
                .WithMany(u => u.GamesPlayer)
                .HasForeignKey(g => g.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(u => u.GamesPlayer)
                .WithOne(g => g.Player);


            modelBuilder.Entity<NavyBattlePiece>()
                .HasOne(g => g.User)
                .WithMany(u => u.Pieces)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Pieces)
                .WithOne(g => g.User);

            modelBuilder.Entity<NavyBattlePiece>()
                .HasOne(g => g.Game)
                .WithMany(u => u.GamePieces)
                .HasForeignKey(g => g.GameId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Game>()
                .HasMany(u => u.GamePieces)
                .WithOne(g => g.Game);
        }
    }
}
