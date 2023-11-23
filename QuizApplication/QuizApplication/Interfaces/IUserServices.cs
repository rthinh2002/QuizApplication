using QuizApplication.Models;

namespace QuizApplication.Interfaces
{
    public interface IUserServices
    {
        User Login(string username, string password);

        void CreateUser(User user);
    }
}
