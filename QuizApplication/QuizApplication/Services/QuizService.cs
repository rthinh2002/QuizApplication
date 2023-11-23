using Newtonsoft.Json;
using QuizApplication.Context;
using QuizApplication.Interfaces;
using QuizApplication.Models;

namespace QuizApplication.Services
{
    public class QuizService : IQuizService
    {
        private ApplicationDbContext _context { get; set; }
        private IHttpContextAccessor _contextAccessor { get; set; }

        public QuizService(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;

        }

        public void AddQuiz(Quiz Quiz)
        {
            Quiz.CreatorId = getUserSessionId();
            Quiz.Id = generateID();
            _context.Add(Quiz);
            _context.SaveChanges();
        }

        public void SubmitUserResponse(UserResponse userResponse, string questionId)
        {
            userResponse.QuestionId = questionId;
            userResponse.UserId = getUserSessionId();
            _context.UserResponses.Add(userResponse);
            _context.SaveChanges();
        }

        public List<Quiz> GetAllQuizzes()
        {
            return _context.Quizzes.ToList();
        }

        public List<Question> GetQuestionByQuizId(string id)
        {
            return _context.Questiones.Where(x => x.QuizId == id).ToList();
        }

        public string GetQuestionCorrectAnswer(string questionId)
        {
            return _context.Questiones.FirstOrDefault(x => x.Id == questionId).CorrectAnswerId;
        }

        public Quiz GetQuizById(string id)
        {
            var content = _context.Quizzes.FirstOrDefault(x => x.Id == id);
            return content;
        }

        public bool UpdateQuiz(string id, Quiz UpdatedQuiz)
        {
            try
            {
                var existingQuiz = _context.Quizzes.FirstOrDefault(q => q.Id == id);

                if (existingQuiz != null)
                {
                    existingQuiz.Title = UpdatedQuiz.Title;
                    existingQuiz.Description = UpdatedQuiz.Description;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool UpdateQuestion(string questionId, Question Question)
        {
            try
            {
                var existingQuestion = _context.Questiones.FirstOrDefault(q => q.Id == questionId);

                if(existingQuestion != null)
                {
                    existingQuestion.Content = Question.Content;
                    existingQuestion.Option1 = Question.Option1;
                    existingQuestion.Option2 = Question.Option2;
                    existingQuestion.Option3 = Question.Option3;
                    existingQuestion.Option4 = Question.Option4;
                    existingQuestion.CorrectAnswerId = Question.CorrectAnswerId;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void AddQuestion(Question question)
        {
            _context.Questiones.Add(question);
            _context.SaveChanges();
        }

        private string getUserSessionId()
        {
            return _contextAccessor.HttpContext.Session.GetString("UserId");
        }

        private string getUserSessionName()
        {
            return _contextAccessor.HttpContext.Session.GetString("UserName");
        }

        private string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }


        public void AddUserScore(string quizId, int TotalCorrectAnswer)
        {
            string scoreBoardId = "";

            // Get scoreboard
            var ScoreBoard = _context.ScoreBoards.FirstOrDefault(s => s.QuizId == quizId);
            scoreBoardId = ScoreBoard == null ? CreateScoreBoard(quizId) : ScoreBoard.Id;

            // Save Score
            UserScore FinalUserScore = new UserScore(
                Math.Abs(Guid.NewGuid().GetHashCode()),
                TotalCorrectAnswer,
                getUserSessionName(),
                scoreBoardId
            );

            _context.Add( FinalUserScore );
            _context.SaveChanges();
        }

        private string CreateScoreBoard(string quizId)
        {
            ScoreBoard newScoreBoard = new ScoreBoard(Guid.NewGuid().ToString(), quizId);
            _context.ScoreBoards.Add(newScoreBoard);
            _context.SaveChanges();
            return newScoreBoard.Id;
        }

        public List<UserScore> GetUserScoreList(string quizId)
        {
            string scoreBoardId = _context.ScoreBoards.FirstOrDefault(q => q.QuizId == quizId).Id;
            return _context.UserScores.Where(scores => scores.ScoreBoardId == scoreBoardId).OrderByDescending(score => score.Score).ToList();
        }
    }
}
