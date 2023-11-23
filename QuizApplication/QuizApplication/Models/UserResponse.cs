using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models
{
    public class UserResponse
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string QuestionId { get; set; }
        public string AnswerId { get; set; }
        public string TimeStamp { get; set; }

        public UserResponse(string id, string answerId, string timeStamp)
        {
            Id = id;
            AnswerId = answerId;
            TimeStamp = timeStamp;
        }
    }
}
