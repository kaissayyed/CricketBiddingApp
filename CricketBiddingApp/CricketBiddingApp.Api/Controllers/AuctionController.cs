using CricketBiddingApp.Api.Data;
using CricketBiddingApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CricketBiddingApp.Api.Controllers
{
    // Controllers/AuctionsController.cs
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly CricketBiddingDbContext _context;

        public AuctionsController(CricketBiddingDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [HttpPost]
        public IActionResult CreateAuction([FromBody] Auction auction)
        {
            // Validate that the start date is in the future and end date is after the start date
            if (auction.StartDateTime < DateTime.UtcNow)
            {
                return BadRequest("Start date must be in the future.");
            }
            if (auction.EndDateTime <= auction.StartDateTime)
            {
                return BadRequest("End date must be after the start date.");
            }

            _context.Auctions.Add(auction);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAuction), new { id = auction.Id }, auction);
        }
        [HttpGet("{id}")]
        public ActionResult<Auction> GetAuction(int id)
        {
            var auction = _context.Auctions.Find(id);
            if (auction == null)
            {
                return NotFound();
            }
            return auction;
        }

        [HttpPost("{auctionId}/bid")]
        public IActionResult PlaceBid(int auctionId, [FromBody] BidRequest bidRequest)
        {
            var auction = _context.Auctions.Include(a => a.Players).FirstOrDefault(a => a.Id == auctionId);
            if (auction == null) return NotFound();

            // Check if the auction is currently active
            if (DateTime.UtcNow < auction.StartDateTime || DateTime.UtcNow > auction.EndDateTime)
            {
                return BadRequest("Bidding is not allowed outside of the auction time.");
            }

            var player = auction.Players.FirstOrDefault(p => p.Id == bidRequest.PlayerId);
            if (player == null || player.IsSold) return BadRequest("Player not available for bidding.");

            var team = _context.Teams.Find(bidRequest.TeamId);
            if (team == null || team.Budget < bidRequest.BidAmount) return BadRequest("Insufficient budget.");

            // If the bid is higher than current bids (if any), process the bid
            // You can implement more logic to handle existing bids
            team.Budget -= bidRequest.BidAmount;
            player.IsSold = true;
            team.Players.Add(player);

            _context.SaveChanges();
            return Ok();
        }
    }


}
