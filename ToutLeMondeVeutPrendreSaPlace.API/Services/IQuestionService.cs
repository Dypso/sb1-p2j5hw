using ToutLeMondeVeutPrendreSaPlace.API.Models;

namespace ToutLeMondeVeutPrendreSaPlace.API.Services
{
    public interface IQuestionService
    {
        Task<List<Question>> GetQuestionsForGameAsync(int count, int difficulty);
        Task<bool> ValidateAnswerAsync(int questionId, string answer);
    }
}