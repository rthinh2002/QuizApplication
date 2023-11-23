using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Interfaces;
using QuizApplication.Models;

namespace QuizApplication.Pages
{
    public class IndexModel : PageModel
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUserServices _userServices { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IndexModel(IUserServices service, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userServices = service;
        }

        public IActionResult OnPost()
        {
            User User = _userServices.Login(UserName, Password);
            if(User != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UserId", User.Id);
                _httpContextAccessor.HttpContext.Session.SetString("UserName", User.UserName);
                return Redirect("/Quizzes");
            }
            return Page();
        }
    }
}