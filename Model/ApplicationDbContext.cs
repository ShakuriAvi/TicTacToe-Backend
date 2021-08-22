using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Player> Player { get; set; }
        public DbSet<Game> Game { get; set; }
    }
}
