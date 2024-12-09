
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
           /* _signInManager = signInManager;
            _userManager = userManager;*/
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
                .FirstOrDefaultAsync(c => c.Email == loginvm.Email);

            if (user == null || user.Password != loginvm.Password) // Replace with hashing logic
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginvm);
            }


            // Store user information in the session
            HttpContext.Session.SetString("UserID", user.AccountID);
            HttpContext.Session.SetString("Usersalutation", user.Salutation??string.Empty);
            HttpContext.Session.SetString("Usersaluteonly", user.SalutationOnly ?? string.Empty);
            HttpContext.Session.SetString("UserFullName", user.FullName);
            HttpContext.Session.SetString("Usertitle", user.Title ?? string.Empty);
            HttpContext.Session.SetString("Userst", user.MailingStreet ?? string.Empty);
            HttpContext.Session.SetString("Usercity", user.MailingCity ?? string.Empty);
            HttpContext.Session.SetString("Userstate", user.MailingState_Province ?? string.Empty);
            HttpContext.Session.SetString("Userpostal", user.MailingZip_PostalCode ?? string.Empty);
            HttpContext.Session.SetString("Usercountry", user.MailingCountry ?? string.Empty);
            HttpContext.Session.SetString("Userphone", user.Phone ?? string.Empty);
            HttpContext.Session.SetString("Usermobile", user.Mobile.ToString() ?? string.Empty);
            HttpContext.Session.SetString("Userfax", user.Fax ?? string.Empty);
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("Userowner", user.AccountOwner ?? string.Empty);
            HttpContext.Session.SetString("Userg1", user.Guardian_1 ?? string.Empty);
            HttpContext.Session.SetString("Userg2", user.Guardian_2 ?? string.Empty);
            HttpContext.Session.SetString("Userg3", user.Guardian_3 ?? string.Empty);
            HttpContext.Session.SetString("Userg4", user.Guardian_4 ?? string.Empty);

            // Redirect to home or desired page
            return RedirectToAction("eventHome", "Home");
        }
    }
}
