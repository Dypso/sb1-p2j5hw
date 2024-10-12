using Microsoft.AspNetCore.Mvc;
using ToutLeMondeVeutPrendreSaPlace.API.Services;

namespace ToutLeMondeVeutPrendreSaPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly IUserService _userService;

        public LeaderboardController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaderboard([FromQuery] int top = 10)
        {
            var leaderboard = await _userService.GetLeaderboardAsync(top);
            return Ok(leaderboard);
        }
    }
}