using Microsoft.AspNetCore.SignalR;
using ToutLeMondeVeutPrendreSaPlace.API.Services;

namespace ToutLeMondeVeutPrendreSaPlace.API.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameService _gameService;

        public GameHub(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task JoinGame(int gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
            await Clients.Group(gameId.ToString()).SendAsync("PlayerJoined", Context.ConnectionId);
        }

        public async Task LeaveGame(int gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
            await Clients.Group(gameId.ToString()).SendAsync("PlayerLeft", Context.ConnectionId);
        }

        public async Task SubmitAnswer(int gameId, int questionId, string answer)
        {
            var userId = int.Parse(Context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
            var result = await _gameService.SubmitAnswerAsync(gameId, userId, questionId, answer);
            await Clients.Group(gameId.ToString()).SendAsync("AnswerSubmitted", new { UserId = userId, Correct = result });
        }

        public async Task StartGame(int gameId)
        {
            var game = await _gameService.GetGameAsync(gameId);
            if (game != null && game.Status == Models.GameStatus.Waiting)
            {
                game.Status = Models.GameStatus.InProgress;
                await _gameService.EndGameAsync(gameId);
                await Clients.Group(gameId.ToString()).SendAsync("GameStarted");
            }
        }

        public async Task EndGame(int gameId)
        {
            await _gameService.EndGameAsync(gameId);
            await Clients.Group(gameId.ToString()).SendAsync("GameEnded");
        }
    }
}