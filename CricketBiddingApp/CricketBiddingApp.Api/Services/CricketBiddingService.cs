// CricketBiddingService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using CricketBiddingApp.Api.Data;
using CricketBiddingApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CricketBiddingApp.Api.Services
{
    public class CricketBiddingService
    {
        private readonly CricketBiddingDbContext _context;

        public CricketBiddingService(CricketBiddingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetAllPlayersAsync()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<List<Auction>> GetAllAuctionsAsync()
        {
            return await _context.Auctions.ToListAsync();
        }

        // Method to refresh all data
        public async Task RefreshAllDataAsync()
        {
            // This will force EF Core to reload the data from the database.
            // It is optional to call the below methods if you want to just keep them for 
            // future use or for re-fetching specific entities.
            await GetAllPlayersAsync();
            await GetAllTeamsAsync();
            await GetAllAuctionsAsync();
        }
    }
}
