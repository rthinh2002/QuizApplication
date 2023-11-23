using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Interfaces;
using QuizApplication.Models;

namespace QuizApplication.Pages
{
    public class QuizzesModel : PageModel
    {
        public List<Quiz> Quizzes { get; set; }

        private IQuizService _quizService {  get; set; }

        public QuizzesModel(IQuizService service)
        {
            _quizService = service;
        }
        public void OnGet()
        {
            Quizzes = _quizService.GetAllQuizzes();
        }
    }
}
