using Microsoft.AspNetCore.Mvc;

namespace clubrainbow.Controllers
{
	public class PasswordController : Controller
	{
		public IActionResult ResetPassword()
		{
			return View();
		}
        public IActionResult checkyouremail()
        {
            return View();
        }

        public IActionResult newpassword()
        {
            return View();
        }

        public IActionResult passwordchanged()
        {
            return View();
        }
    }
}
