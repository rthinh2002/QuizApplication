using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models
{
    public class ScoreBoard
    {
        [Key]
        public string Id { get; set; }
        public string QuizId { get; set; }
        public ICollection<UserScore> UserScores { get; } = new List<UserScore>();

        public ScoreBoard(string id, string quizId)
        {
            Id = id;
            QuizId = quizId;
        }
    }
}
