using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ToutLeMondeVeutPrendreSaPlace.API.Data;
using ToutLeMondeVeutPrendreSaPlace.API.Models;

namespace ToutLeMondeVeutPrendreSaPlace.API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<User> RegisterAsync(string username, string email, string password)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                throw new Exception("Username is already taken");

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<List<User>> GetLeaderboardAsync(int top)
        {
            return await _context.Users
                .OrderByDescending(u => u.TotalScore)
                .Take(top)
                .ToListAsync();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return storedHash == HashPassword(password);
        }
    }
}