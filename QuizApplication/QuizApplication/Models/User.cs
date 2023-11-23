using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string UserName {  get; set; }
        public string UserEmail { get; set; }   
        public int Phone { get; set; }
        public string HashPassword { get; set; }

        public void generateID()
        {
            Id = Guid.NewGuid().ToString("N");
        }

    }
}
