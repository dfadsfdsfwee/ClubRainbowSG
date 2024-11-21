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
            
            if (email == "user" && password == "password")  
            {
                
                return RedirectToAction("eventHome", "Home");  
            }

           
            ViewBag.ErrorMessage = "Invalid credentials.";
            return View();
        }
    }
}
