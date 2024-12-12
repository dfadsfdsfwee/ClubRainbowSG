
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClubRainbowSG.Models;
using Microsoft.EntityFrameworkCore;

using clubrainbowSG.Data;
using Microsoft.AspNetCore.Http;

namespace ClubRainbowSG.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;


        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            
            return View();
            
        }
        [HttpPost]
        
        public async Task<IActionResult> Login(LoginVM loginvm)
        {
            if (!ModelState.IsValid)
                return View(loginvm);

            var user = await _context.Contacts
                .FirstOrDefaultAsync(c => c.email == loginvm.Email);

            if (user == null || user.hashed_password != loginvm.Password) // Replace with hashing logic
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginvm);
            }


            // Store user information in the session
            HttpContext.Session.SetString("UserID", user.account_name);
            HttpContext.Session.SetString("Usersalutation", user.salutation.ToString()??string.Empty);
            HttpContext.Session.SetString("Usersaluteonly", user.salutation_Only ?? string.Empty);
            HttpContext.Session.SetString("UserFullName", user.full_name!);
            HttpContext.Session.SetString("Usertitle", user.title ?? string.Empty);
            HttpContext.Session.SetString("Userst", user.mailing_street ?? string.Empty);
            HttpContext.Session.SetString("Usercity", user.mailing_city ?? string.Empty);
            HttpContext.Session.SetString("Userstate", user.mailing_state_provence ?? string.Empty);
            HttpContext.Session.SetString("Userpostal", user.mailing_zip_postal ?? string.Empty);
            HttpContext.Session.SetString("Usercountry", user.mailing_country ?? string.Empty);
            HttpContext.Session.SetString("Userphone", user.phone ?? string.Empty);
            HttpContext.Session.SetString("Usermobile", user.mobile.ToString() ?? string.Empty);
            HttpContext.Session.SetString("Userfax", user.fax.ToString() ?? string.Empty);
            HttpContext.Session.SetString("UserEmail", user.email!);
            HttpContext.Session.SetString("Userowner", user.account_owner ?? string.Empty);
            HttpContext.Session.SetString("Userg1", user.Guardian_1 ?? string.Empty);
            HttpContext.Session.SetString("Userg2", user.Guardian_2 ?? string.Empty);
            HttpContext.Session.SetString("Userg3", user.Guardian_3 ?? string.Empty);
            HttpContext.Session.SetString("Userg4", user.Guardian_4 ?? string.Empty);

            // Redirect to home or desired page
            return RedirectToAction("eventHome", "Home");
        }
        
        public IActionResult Logout()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Logoutuser()
        {

            HttpContext.Session.Clear();

            // Redirect to login page or home page
            return RedirectToAction("Login", "Account");
        }
    }
}
