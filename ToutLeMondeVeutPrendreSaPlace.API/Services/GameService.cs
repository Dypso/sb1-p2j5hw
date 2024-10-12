using Microsoft.EntityFrameworkCore;
using ToutLeMondeVeutPrendreSaPlace.API.Data;
using ToutLeMondeVeutPrendreSaPlace.API.Models;

namespace ToutLeMondeVeutPrendreSaPlace.API.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuestionService _questionService;

        public GameService(ApplicationDbContext context, IQuestionService questionService)
        {
            _context = context;
            _questionService = questionService;
        }

        public async Task<Game> CreateGameAsync()
        {
            var game = new Game
            {
                StartTime = DateTime.UtcNow,
                Status = GameStatus.Waiting,
                Players = new List<GamePlayer>()
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return game;
        }

        public async Task<Game> JoinGameAsync(int userId, int gameId)
        {
            var game = await _context.Games
                .Include(g => g.Players)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null)
                throw new Exception("Game not found");

            if (game.Status != GameStatus.Waiting)
                throw new Exception("Game is not in waiting state");

            if (game.Players.Any(p => p.UserId == userId))
                throw new Exception("User already joined this game");

            game.Players.Add(new GamePlayer { UserId = userId, GameId = gameId });
            await _context.SaveChangesAsync();

            return game;
        }

        public async Task<Game> GetGameAsync(int gameId)
        {
            return await _context.Games
                .Include(g => g.Players)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(g => g.Id == gameId);
        }

        public async Task<bool> SubmitAnswerAsync(int gameId, int userId, int questionId, string answer)
        {
            var game = await _context.Games
                .Include(g => g.Players)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null || game.Status != GameStatus.InProgress)
                throw new Exception("Invalid game state");

            var player = game.Players.FirstOrDefault(p => p.UserId == userId);
            if (player == null)
                throw new Exception("Player not found in this game");

            var isCorrect = await _questionService.ValidateAnswerAsync(questionId, answer);
            if (isCorrect)
            {
                player.Score += 1;
                await _context.SaveChangesAsync();
            }

            return isCorrect;
        }

        public async Task EndGameAsync(int gameId)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game == null)
                throw new Exception("Game not found");

            game.Status = GameStatus.Finished;
            game.EndTime = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}