using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Interfaces;
using QuizApplication.Models;
using Newtonsoft.Json;

namespace QuizApplication.Pages
{
    public class QuizModel : PageModel
    {
        [BindProperty]
        public Quiz? Quiz { get; set; }

        public string QuizId { get; set; }

        [BindProperty]
        public List<Question> QuestionList { get; set; }

        private IQuizService _quizService {  get; set; }

        private IHttpContextAccessor _httpContextAccessor { get; set; }

        public QuizModel(IQuizService service, IHttpContextAccessor httpContextAccessor)
        {
            _quizService = service;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet([FromQuery] string id)
        {
            Quiz = _quizService.GetQuizById(id);
            QuizId = id;
            QuestionList = _quizService.GetQuestionByQuizId(id);
            _httpContextAccessor.HttpContext.Session.SetString("QuestionList", JsonConvert.SerializeObject(QuestionList));
        }

        public IActionResult OnPost()
        {
            bool UpdateSuccess = _quizService.UpdateQuiz(Quiz.Id, Quiz);
            if(UpdateSuccess)
            {
                return Redirect("/Quizzes");
            }

            return Page();
        }

        public IActionResult OnPostSaveQuestions()
        {
            int questionCount = int.Parse(Request.Form["QuestionCount"]);

            var questionListJson = HttpContext.Session.GetString("QuestionList");
            var previousQuestionList = JsonConvert.DeserializeObject<List<Question>>(questionListJson);


            if (questionCount != previousQuestionList.Count) // Add first
            {
                for (int i = previousQuestionList.Count + 1; i <= questionCount; i++)
                {
                    var question = new Question(
                        Guid.NewGuid().ToString(),
                        Request.Form["QuizId"],
                        Request.Form["Question" + i],
                        Request.Form["correctAnswer_" + i],
                        Request.Form["Option1_" + i],
                        Request.Form["Option2_" + i],
                        Request.Form["Option3_" + i],
                        Request.Form["Option4_" + i]
                    );

                    _quizService.AddQuestion(question);
                }
            }

            bool updateSuccess = true;
            int index = 1;
            // Update 
            foreach (var question in previousQuestionList)
            {
                var updatedQuestion = new Question(
                        Guid.NewGuid().ToString(),
                        Request.Form["QuizId"],
                        Request.Form["Question" + index],
                        Request.Form["correctAnswer_" + index],
                        Request.Form["Option1_" + index],
                        Request.Form["Option2_" + index],
                        Request.Form["Option3_" + index],
                        Request.Form["Option4_" + index]
                );
                updateSuccess = _quizService.UpdateQuestion(question.Id, updatedQuestion);
                if (!updateSuccess)
                {
                    break;
                }
                index++;
            }

            if (!updateSuccess)
            {
                return Page();
            }
            return Redirect("/Quizzes");
        }
    }
}
