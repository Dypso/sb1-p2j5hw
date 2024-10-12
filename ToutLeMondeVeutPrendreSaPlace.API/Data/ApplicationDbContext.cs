using Microsoft.EntityFrameworkCore;
using ToutLeMondeVeutPrendreSaPlace.API.Models;

namespace ToutLeMondeVeutPrendreSaPlace.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GamePlayer> GamePlayers { get; set; }
    }
}