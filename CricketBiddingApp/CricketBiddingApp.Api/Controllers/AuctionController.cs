using CricketBiddingApp.Api.Data;
using CricketBiddingApp.Api.Models;
using CricketBiddingApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CricketBiddingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly CricketBiddingDbContext _context;
        private readonly IAuctionService _auctionService;

        public AuctionsController(CricketBiddingDbContext context, IAuctionService auctionService) 
        {
            _context = context;
            _auctionService = auctionService;
        }

        // GET: api/auctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAllAuctions()
        {
            var auctions = await _auctionService.GetAllAuctionsAsync();
            if (auctions == null || auctions.Count == 0)
            {
                return NotFound("No auctions found.");
            }

            return Ok(auctions);
        }

        // POST: api/auctions
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

        // GET: api/auctions/{id}
        [HttpGet("{id}")]
        public ActionResult<Auction> GetAuction(int id)
        {
            var auction = _context.Auctions
                .Include(a => a.Players) // Include related players
                .FirstOrDefault(a => a.Id == id);

            if (auction == null)
            {
                return NotFound("Auction not found.");
            }

            return auction;
        }

        // POST: api/auctions/{id}/bid
        [HttpPost("{id}/bid")]
        public IActionResult PlaceBid(int id, [FromBody] BidRequest bid)
        {
            var auction = _context.Auctions
                .Include(a => a.Players) // Include players
                .FirstOrDefault(a => a.Id == id);

            if (auction == null)
            {
                return NotFound("Auction not found.");
            }

            var player = auction.Players.FirstOrDefault(p => p.Id == bid.PlayerId);
            var team = _context.Teams.FirstOrDefault(t => t.Id == bid.TeamId);

            if (player == null || team == null)
            {
                return BadRequest("Invalid player or team.");
            }

            // Check if the bid is higher than the current bid
            if (bid.BidAmount <= player.CurrentBid)
            {
                return BadRequest("Bid amount must be higher than the current bid.");
            }

            // Check if the team has enough budget
            if (team.Budget < bid.BidAmount)
            {
                return BadRequest("Team does not have enough budget.");
            }

            // Update the player with the new highest bid
            player.CurrentBid = bid.BidAmount;
            player.WinningTeamId = bid.TeamId;

            // Mark player as sold
            player.IsSold = true;

            // Deduct the bid amount from the team's budget
            team.Budget -= bid.BidAmount;

            _context.SaveChanges();

            return Ok("Bid placed successfully.");
        }
    }
}
