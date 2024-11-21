using Microsoft.AspNetCore.Mvc;

namespace ClubRainbowSG.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            // Example login validation (replace with real authentication logic)
            if (email == "user" && password == "password")  // Your login logic
            {
                // Redirect to another controller and view after successful login
                return RedirectToAction("eventHome", "Home");  // Redirect to "EventHome" action in "Home" controller
            }

            // If login fails, return to the login page with an error message
            ViewBag.ErrorMessage = "Invalid credentials.";
            return View();
        }
    }
}
