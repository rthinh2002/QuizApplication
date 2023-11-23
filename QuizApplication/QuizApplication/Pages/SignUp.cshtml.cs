using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApplication.Interfaces;
using QuizApplication.Models;

namespace QuizApplication.Pages
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public User? User { get; set; }
        private IUserServices _userServices { get; set; }

        public SignUpModel(IUserServices service)
        {
            _userServices = service;
        }
        public IActionResult OnPost()
        {
            User.generateID();
            _userServices.CreateUser(User);
            return Redirect("/Index");
        }
    }
}
