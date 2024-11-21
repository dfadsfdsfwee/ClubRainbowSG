using Microsoft.AspNetCore.Mvc;

namespace ClubRainbowSG.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
            
        }
    }
}
