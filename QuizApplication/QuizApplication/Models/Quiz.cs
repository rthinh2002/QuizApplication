using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models
{
    public class Quiz
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatorId { get; set; }
    }
}
