using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Interfaces;
using QuizApplication.Models;

namespace QuizApplication.Pages
{
    public class ScoreBoardModel : PageModel
    {
        private IQuizService _quizService { get; set; }

        public List<UserScore> UserScoresList { get; set; }

        public ScoreBoardModel(IQuizService service)
        {
            _quizService = service;
        }

        public void OnGet([FromQuery] string id)
        {
            UserScoresList = _quizService.GetUserScoreList(id);
        }
    }
}
