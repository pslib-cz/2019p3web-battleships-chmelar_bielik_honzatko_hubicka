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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
            .HasMany(u => u.GamesPlay)
            .WithOne(g => g.CurrentPlayer);
        }
    }
}
