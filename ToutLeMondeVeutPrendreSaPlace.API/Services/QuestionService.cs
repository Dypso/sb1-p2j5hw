using Microsoft.EntityFrameworkCore;
using ToutLeMondeVeutPrendreSaPlace.API.Data;
using ToutLeMondeVeutPrendreSaPlace.API.Models;

namespace ToutLeMondeVeutPrendreSaPlace.API.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _context;

        public QuestionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetQuestionsForGameAsync(int count, int difficulty)
        {
            return await _context.Questions
                .Where(q => q.Difficulty == difficulty)
                .OrderBy(r => Guid.NewGuid())
                .Take(count)
                .ToListAsync();
        }

        public async Task<bool> ValidateAnswerAsync(int questionId, string answer)
        {
            var question = await _context.Questions.FindAsync(questionId);
            return question != null && question.CorrectAnswer == answer;
        }
    }
}