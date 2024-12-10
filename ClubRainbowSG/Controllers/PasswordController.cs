using clubrainbowSG.Data;
using ClubRainbowSG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace clubrainbow.Controllers
{
	public class PasswordController : Controller
	{
        private readonly ApplicationDbContext _context;
        public PasswordController(ApplicationDbContext context)
        {
            _context = context;
        }
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
            ViewBag.email = HttpContext.Session.GetString("UserEmail");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> newpassword(string newPW, string confPW,string email)
        {
           
            if (email == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (newPW != confPW)
            {
                ModelState.AddModelError("matchPassword", "New password and confirm password do not match.");
                return View();
            }
            user.Password = newPW; // Update password (consider hashing it before saving)
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult passwordchanged()
        {
            return View();
        }
    }
}
