using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using QuizApplication.Interfaces;
using QuizApplication.Models;

namespace QuizApplication.Pages
{
    public class StartQuizModel : PageModel
    {
        public List<Question> QuestionsList { get; set; }

        [BindProperty]
        public Quiz? Quiz { get; set; }

        public string QuizId { get; set; }

        private IQuizService _quizService {  get; set; }
        private IHttpContextAccessor _contextAccessor { get; set; }

        public StartQuizModel(IQuizService service, IHttpContextAccessor contextAccessor)
        {
            _quizService = service;
            _contextAccessor = contextAccessor;
        }

        public void OnGet([FromQuery] string Id)
        {
            Quiz = _quizService.GetQuizById(Id);
            QuestionsList = _quizService.GetQuestionByQuizId(Id);
            QuizId = Id;
            _contextAccessor.HttpContext.Session.SetString("QuestionList", JsonConvert.SerializeObject(QuestionsList));
        }

        public IActionResult OnPost()
        {
            int TotalCorrectAnswer = 0;
            var questionListJson = HttpContext.Session.GetString("QuestionList");
            var questionList = JsonConvert.DeserializeObject<List<Question>>(questionListJson);

            for(int i = 1; i <= questionList.Count(); i++)
            {
                string questionId = Request.Form["QuestionId_" + i];
                string userResult = Request.Form[questionId + " _" + i];
                string correctAnswer = _quizService.GetQuestionCorrectAnswer(questionId);

                UserResponse userResponse = new UserResponse(
                    Guid.NewGuid().ToString(),
                    userResult,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                );

                _quizService.SubmitUserResponse(userResponse, questionId);

                // Calculate score
                if(userResult == correctAnswer)
                {
                    TotalCorrectAnswer++;
                }
            }

            // Save Score
            _quizService.AddUserScore(Request.Form["QuizId"], TotalCorrectAnswer);
            string quizId = Request.Form["QuizId"];
            return RedirectToPage("/ScoreBoard", new { id = quizId });
        }
    }
}
