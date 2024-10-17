using CricketBiddingApp.Api.Data;
using CricketBiddingApp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricketBiddingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly CricketBiddingDbContext _context;

        public PlayersController(CricketBiddingDbContext context)
        {
            _context = context;
        }

        // GET: api/players
        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetPlayers()
        {
            return _context.Players.ToList();
        }

        // GET: api/players/{id}
        [HttpGet("{id}")]
        public ActionResult<Player> GetPlayer(int id)
        {
            var player = _context.Players.Find(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        [HttpGet("{categoryId}")]
        public ActionResult<Player> GetPlayerByCategory(int categoryId)
        {
            var player = _context.Players.Find(categoryId);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // POST: api/players
        [HttpPost]
        public ActionResult<Player> CreatePlayer([FromBody] Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Players.Add(player);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
        }

        // GET: api/players/sold
        [HttpGet("sold")]
        public ActionResult<IEnumerable<Player>> GetSoldPlayers()
        {
            var soldPlayers = _context.Players.Where(p => p.IsSold == true).ToList();
            return soldPlayers;
        }

        // GET: api/players/unsold
        [HttpGet("unsold")]
        public ActionResult<IEnumerable<Player>> GetUnsoldPlayers()
        {
            var unsoldPlayers = _context.Players.Where(p => p.IsSold == false).ToList();
            return unsoldPlayers;
        }
    }
}