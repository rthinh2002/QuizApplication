using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models
{
    public class Question
    {
        [Key]
        public string Id { get; set; }
        public string QuizId { get; set; }
        public string Content { get; set; }
        public string CorrectAnswerId { get; set; }

        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        public Question(string id, string quizId, string content, string correctAnswerId, string option1, string option2, string option3, string option4)
        {
            Id = id;
            QuizId = quizId;
            Content = content;
            CorrectAnswerId = correctAnswerId;
            Option1 = option1;
            Option2 = option2;
            Option3 = option3;
            Option4 = option4;
        }
    }
}
