using ToutLeMondeVeutPrendreSaPlace.API.Models;

namespace ToutLeMondeVeutPrendreSaPlace.API.Services
{
    public interface IGameService
    {
        Task<Game> CreateGameAsync();
        Task<Game> JoinGameAsync(int userId, int gameId);
        Task<Game> GetGameAsync(int gameId);
        Task<bool> SubmitAnswerAsync(int gameId, int userId, int questionId, string answer);
        Task EndGameAsync(int gameId);
    }
}