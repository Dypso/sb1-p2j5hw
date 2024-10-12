using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToutLeMondeVeutPrendreSaPlace.API.Services;

namespace ToutLeMondeVeutPrendreSaPlace.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGame()
        {
            var game = await _gameService.CreateGameAsync();
            return Ok(new { GameId = game.Id });
        }

        [HttpPost("join/{gameId}")]
        public async Task<IActionResult> JoinGame(int gameId)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
            var game = await _gameService.JoinGameAsync(userId, gameId);
            return Ok(new { Message = "Joined game successfully" });
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame(int gameId)
        {
            var game = await _gameService.GetGameAsync(gameId);
            if (game == null)
                return NotFound();
            return Ok(game);
        }

        [HttpPost("{gameId}/submit")]
        public async Task<IActionResult> SubmitAnswer(int gameId, [FromBody] AnswerModel model)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
            var result = await _gameService.SubmitAnswerAsync(gameId, userId, model.QuestionId, model.Answer);
            return Ok(new { Correct = result });
        }

        [HttpPost("{gameId}/end")]
        public async Task<IActionResult> EndGame(int gameId)
        {
            await _gameService.EndGameAsync(gameId);
            return Ok(new { Message = "Game ended successfully" });
        }
    }

    public class AnswerModel
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}