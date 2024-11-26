using System.Diagnostics;
using clubrainbowSG.Data;
using ClubRainbowSG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClubRainbowSG.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> AddAccount()
        {
            Console.WriteLine("AddAccount method is being called");


            var account = new User
            {
                Username = "johntest",
                Email = "johndoe@example.com",
                CreatedDate = DateTime.Now
            };

            _context.Account.Add(account);


            await _context.SaveChangesAsync();

            return Ok("Account added successfully");
        }

        public IActionResult eventHome(string pcsDropdown)
        {
            var programmes = _context.TestProgrammes.ToList();

            // If no results, log this information
            if (programmes == null || !programmes.Any())
            {
                Console.WriteLine("No programmes found in the database.");
            }

            // Extract PCSnames
            var pcsNames = programmes.Select(p => p.PCSname).ToList();

            // Log the fetched PCSnames
            Console.WriteLine("PCSNames fetched: " + string.Join(", ", pcsNames));
            string eventName = string.IsNullOrEmpty(pcsDropdown) ? "" : pcsDropdown;
            // Passing data to View
            ViewBag.PCSNames = pcsNames;
            
            ViewBag.EventName = eventName;
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
