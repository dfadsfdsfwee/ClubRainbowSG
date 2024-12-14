using clubrainbowSG.Data;
using ClubRainbowSG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace clubrainbow.Controllers
{
	public class PasswordController : Controller
	{
        private readonly ApplicationDbContext _context;
        public PasswordController(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GenerateTemporaryToken()
        {
            // Generate a GUID token
            return Guid.NewGuid().ToString();
        }
        private bool IsValidEmail(string email)
        {
            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email)
        {
            if (string.IsNullOrEmpty(email)||!IsValidEmail(email))
            {
                ModelState.AddModelError("", "valid Email is required");
                return View();
            }
            var token=GenerateTemporaryToken();
            var user = await _context.Contacts
                .FirstOrDefaultAsync(c => c.email == email);

            if (user == null) // Replace with hashing logic
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }
            var client = new HttpClient();
            var url = "https://prod-00.southeastasia.logic.azure.com:443/workflows/d1f92f299d004c37860b82778ad85252/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=ZK1wX5eTgJNhQ1feYfnZAMFq8M3cej7Hlkj_7sEZweY";
            var payload = new
            {
                email = email,
                encryptedEmail = token
            };
            var jsonString = JsonSerializer.Serialize(payload);
            var content = new StringContent(
                jsonString,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {   HttpContext.Session.SetString("forpasschange", token);
                // Redirect to a success page
                return RedirectToAction("checkyouremail");
                
            }

            // Handle failure response from Power Automate
            ModelState.AddModelError("", "Failed to send email.");
            return View();
        }
        public IActionResult checkyouremail()
        {
            return View();
        }

        public IActionResult newpassword(string email,string token )
        {
            ViewBag.encrypted = token;//??
            ViewBag.email = email;
            ViewBag.checktoken=HttpContext.Session.GetString("forpasschange");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> newpassword(string newPW, string confPW,string email)
        {
           
            if (email == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = await _context.Contacts.FirstOrDefaultAsync(c => c.email == email);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (newPW != confPW)
            {
                ModelState.AddModelError("matchPassword", "New password and confirm password do not match.");
                return View();
            }
            user.hashed_password = newPW; // Update password (consider hashing it before saving)
            await _context.SaveChangesAsync();
            return RedirectToAction("passwordchanged", "Password");
        }

        public IActionResult passwordchanged()
        {
            return View();
        }
    }
}
