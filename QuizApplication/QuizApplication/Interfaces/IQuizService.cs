using QuizApplication.Models;

namespace QuizApplication.Interfaces
{
    public interface IQuizService
    {
        List<Quiz> GetAllQuizzes();

        List<Question> GetQuestionByQuizId(string id);

        void AddQuiz(Quiz quiz);

        void AddQuestion(Question question);

        void AddUserScore(string quizId, int TotalCorrectAnswer);

        void SubmitUserResponse(UserResponse userResponse, string questionId);

        List<UserScore> GetUserScoreList(string quizId);

        string GetQuestionCorrectAnswer(string questionId);
        Quiz GetQuizById(string id);

        bool UpdateQuiz(string id, Quiz UpdatedQuiz);

        bool UpdateQuestion(string quizId, Question UpdatedQuestion);

    }
}
