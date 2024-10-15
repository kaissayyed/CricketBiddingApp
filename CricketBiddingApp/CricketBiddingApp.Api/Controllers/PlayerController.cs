using CricketBiddingApp.Api.Data;
using CricketBiddingApp.Api.Models;
using CricketBiddingApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace CricketBiddingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly CricketBiddingDbContext _context;
        private Logger Logger = LogManager.GetCurrentClassLogger();

        public PlayersController(CricketBiddingDbContext context)
        {
            _context = context;
        }

        // POST: api/Players
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player players)
        {
            try
            {
                _context.Players.Add(players);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPlayer), new { id = players.Id }, players);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return BadRequest(ex.Message);
            }
           
        }

        // GET: api/Players/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players.Include(p => p.Teams).FirstOrDefaultAsync(p => p.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            return player;
        }

        [HttpPost("bid/{playerId}/{teamId}")]
        public async Task<ActionResult> PlaceBid(int playerId, int teamId, decimal bidAmount)
        {
            var auctionService = new AuctionService(_context);
            var result = await auctionService.BidOnPlayer(playerId, teamId, bidAmount);

            if (result is BadRequestResult)
            {
                return BadRequest("Error in bidding: " + result);
            }

            return Ok("Bid placed successfully.");
        }

        [HttpGet("players/sold")]
        public async Task<ActionResult<List<Player>>> GetSoldPlayers()
        {
            var soldPlayers = await _context.Players.Include(p => p.Teams)
                                                    .Where(p => p.IsSold)
                                                    .ToListAsync();
            return Ok(soldPlayers);
        }

        [HttpGet("players/unsold")]
        public async Task<ActionResult<List<Player>>> GetUnsoldPlayers()
        {
            var unsoldPlayers = await _context.Players.Include(p => p.Teams)
                                                      .Where(p => !p.IsSold)
                                                      .ToListAsync();
            return Ok(unsoldPlayers);
        }

    }

}
