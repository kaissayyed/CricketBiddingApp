using CricketBiddingApp.Api.Data;
using CricketBiddingApp.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CricketBiddingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly CricketBiddingDbContext _context;

        public AuctionController(CricketBiddingDbContext context)
        {
            _context = context;
        }

        // POST: api/Auction
        [HttpPost]
        public async Task<ActionResult<Auction>> CreateAuction(Auction auction)
        {
            // Validate the auction details
            if (auction.StartTime >= auction.EndTime)
            {
                return BadRequest("Start time must be earlier than end time.");
            }

            auction.IsActive = true; // Set auction as active
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuction), new { id = auction.Id }, auction);
        }

        // GET: api/Auction/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Auction>> GetAuction(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);

            if (auction == null)
            {
                return NotFound();
            }

            return auction;
        }

        // Optional: GET: api/Auction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAllAuctions()
        {
            var auctions = await _context.Auctions.ToListAsync();
            return Ok(auctions);
        }
    }

}
