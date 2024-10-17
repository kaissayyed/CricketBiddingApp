using Microsoft.AspNetCore.Mvc;
using CricketBiddingApp.Api.Services;
using System.Threading.Tasks;

namespace CricketBiddingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefreshController : ControllerBase
    {
        private readonly CricketBiddingService _service;

        public RefreshController(CricketBiddingService service)
        {
            _service = service;
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshDatabase()
        {
            await _service.RefreshAllDataAsync();
            return Ok("Database refreshed successfully.");
        }
    }
}
