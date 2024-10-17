using CricketBiddingApp.Api.Data;
using CricketBiddingApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CricketBiddingApp.Api.Services
{
    public interface IAuctionService
    {
        Task<List<Auction>> GetAllAuctionsAsync();
    }
    public class AuctionService : IAuctionService
    {
        private readonly CricketBiddingDbContext _context;

        public AuctionService(CricketBiddingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Auction>> GetAllAuctionsAsync()
        {
            return await _context.Auctions
                .Include(a => a.Players) // Assuming there is a Players navigation property
                .Include(a => a.Teams)   // Assuming there is a Teams navigation property
                .ToListAsync();
        }
    }
}
