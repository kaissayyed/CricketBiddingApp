using Microsoft.EntityFrameworkCore;
using CricketBiddingApp.Api.Models;

namespace CricketBiddingApp.Api.Data
{
    public class CricketBiddingDbContext : DbContext
    {
        public CricketBiddingDbContext(DbContextOptions<CricketBiddingDbContext> options) : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Auction> Auctions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .ToTable("Team");
            modelBuilder.Entity<Player>()
                .ToTable("Player");
            modelBuilder.Entity<Auction>()
                .ToTable("Auction");
            base.OnModelCreating(modelBuilder);
        }
        
    }

    

}
