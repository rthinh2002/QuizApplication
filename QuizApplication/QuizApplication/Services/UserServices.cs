using QuizApplication.Context;
using QuizApplication.Interfaces;
using QuizApplication.Models;

namespace QuizApplication.Services
{
    public class UserServices : IUserServices
    {
        private ApplicationDbContext _context { get; set; }

        public UserServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User Login(string username, string password)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserName == username);
            if(PassWordVerification(user, password))
            {
                return user;
            }
            return null;
        }

        private bool PassWordVerification(User user, string password) 
        { 
            if(user == null)
            {
                return false;
            }

            return user.HashPassword == password;
        }
    }
}
