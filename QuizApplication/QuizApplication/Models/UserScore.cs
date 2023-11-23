using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Models
{
    [Table("UserScore")]
    public class UserScore
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string UserName { get; set; }
        public string ScoreBoardId { get; set; }

        public ScoreBoard ScoreBoard { get; set; } = null!;

        public UserScore(int id, int score, string userName, string scoreBoardId)
        {
            Id = id;
            Score = score;
            UserName = userName;
            ScoreBoardId = scoreBoardId;
        }
    }
}
