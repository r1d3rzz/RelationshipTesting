using Microsoft.AspNetCore.Mvc;
using Relationship.Models;

namespace Relationship.Controllers
{
    public class AuthController : Controller
    {
        private readonly RelationshipContext context;

        public AuthController(RelationshipContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var authUser = context.Users.Where(x => x.Name == user.Name).FirstOrDefault();
            if(authUser != null)
            {
                HttpContext.Session.SetInt32("AuthUserId", authUser.Id);
                return Redirect("/");
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Logout() {
            if (HttpContext.Session.Keys.Contains("AuthUserId"))
            {
                HttpContext.Session.Remove("AuthUserId");
                return RedirectToAction("Login");
            }
            return Redirect("/");
        }
    }
}
