using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Interfaces;
using QuizApplication.Models;

namespace QuizApplication.Pages
{
    public class AddQuizModel : PageModel
    {
        [BindProperty]
        public Quiz? Quiz { get; set; }
        private IQuizService _quizService { get; set; }

        public AddQuizModel(IQuizService service)
        {
            _quizService = service;
        }

        public IActionResult OnPost()
        {
            _quizService.AddQuiz(Quiz);
            
            return Redirect("/Quizzes");
        }
    }
}
