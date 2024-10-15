using CricketBiddingApp.Api.Data;
using CricketBiddingApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;

namespace CricketBiddingApp.Api.Services
{
    public class AuctionService
    {
        private readonly CricketBiddingDbContext _context;
        Logger Logger = LogManager.GetCurrentClassLogger();

        public AuctionService(CricketBiddingDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> BidOnPlayer(int playerId, int teamId, decimal bidAmount)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);

            if (player == null || team == null || player.IsSold)
            {
                return new BadRequestObjectResult("Invalid player or team, or player already sold.");
            }

            var auctionRules = await _context.AuctionRules.FirstOrDefaultAsync();

            // Check if the team has reached its player limit
            if (team.PlayerCount >= auctionRules.PlayerLimit)
            {
                return new BadRequestObjectResult("Team has reached the player limit.");
            }

            // Check if the bid amount exceeds the team's remaining budget
            if (team.BudgetSpent + bidAmount > auctionRules.MaxBudget)
            {
                return new BadRequestObjectResult("Bid exceeds the team's budget.");
            }

            // Mark player as sold
            player.IsSold = true;
            player.TeamId = teamId;
            player.SoldPrice = bidAmount;

            // Update team's budget and player count
            team.BudgetSpent += bidAmount;
            team.PlayerCount++;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }

}
