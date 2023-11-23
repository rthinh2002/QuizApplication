using Microsoft.EntityFrameworkCore;
using QuizApplication.Models;

namespace QuizApplication.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScoreBoard>()
                .HasMany(e => e.UserScores)
                .WithOne(e => e.ScoreBoard)
                .HasForeignKey(e => e.ScoreBoardId)
                .IsRequired();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questiones { get; set; }
        public DbSet<ScoreBoard> ScoreBoards { get; set; }
        public DbSet<UserResponse> UserResponses { get; set; }

        public DbSet<UserScore> UserScores { get; set; }
    }
}
