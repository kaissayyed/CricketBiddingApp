using CricketBiddingApp.Api.Data;
using CricketBiddingApp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CricketBiddingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly CricketBiddingDbContext _context;

        public TeamsController(CricketBiddingDbContext context)
        {
            _context = context;
        }

        // GET: api/teams
        [HttpGet]
        public ActionResult<IEnumerable<Team>> GetTeams()
        {
            return _context.Teams.ToList();
        }

        // GET: api/teams/{id}
        [HttpGet("{id}")]
        public ActionResult<Team> GetTeam(int id)
        {
            var team = _context.Teams.Find(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // POST: api/teams
        [HttpPost]
        public ActionResult<Team> CreateTeam([FromBody] Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Teams.Add(team);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
        }
    }
}
