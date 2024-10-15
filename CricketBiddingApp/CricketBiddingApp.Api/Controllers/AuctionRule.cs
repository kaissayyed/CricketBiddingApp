using CricketBiddingApp.Api.Data;
using CricketBiddingApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CricketBiddingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionRuleController : ControllerBase
    {
        private readonly CricketBiddingDbContext _context;

        public AuctionRuleController(CricketBiddingDbContext context)
        {
            _context = context;
        }

        // GET: api/AuctionRule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionRule>>> GetAuctionRules()
        {
            return await _context.AuctionRules.ToListAsync();
        }

        // GET: api/AuctionRule/id
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionRule>> GetAuctionRule(int id)
        {
            var auctionRule = await _context.AuctionRules.FindAsync(id);

            if (auctionRule == null)
            {
                return NotFound();
            }

            return auctionRule;
        }

        // POST: api/AuctionRule
        [HttpPost]
        public async Task<ActionResult<AuctionRule>> CreateAuctionRule(AuctionRule auctionRule)
        {
            _context.AuctionRules.Add(auctionRule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuctionRule), new { id = auctionRule.Id }, auctionRule);
        }

        // PUT: api/AuctionRule/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuctionRule(int id, AuctionRule auctionRule)
        {
            if (id != auctionRule.Id)
            {
                return BadRequest();
            }

            _context.Entry(auctionRule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionRuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/AuctionRule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuctionRule(int id)
        {
            var auctionRule = await _context.AuctionRules.FindAsync(id);
            if (auctionRule == null)
            {
                return NotFound();
            }

            _context.AuctionRules.Remove(auctionRule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuctionRuleExists(int id)
        {
            return _context.AuctionRules.Any(e => e.Id == id);
        }
    }
}
