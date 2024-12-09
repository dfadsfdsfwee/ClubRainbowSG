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
            var userEmail = HttpContext.Session.GetString("UserFullName");

            // Pass session data to ViewBag
            ViewBag.UserEmail = userEmail;
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
            // Fetch all programmes
            var programmes = _context.TestProgram.ToList();

            // Log if no programmes are found
            if (programmes == null || !programmes.Any())
            {
                Debug.WriteLine("No programmes found in the database.");
                return View(); // Return default view if no data
            }

            // Log available PSCNames
            var pscNames = programmes.Select(p => p.PSCName).ToList();
            Debug.WriteLine("Available PSCNames: " + string.Join(", ", pscNames));

            // Trim and normalize pcsDropdown
            string eventName = string.IsNullOrEmpty(pcsDropdown) ? "" : pcsDropdown.Trim();
            Debug.WriteLine("pcsDropdown Value: " + eventName);

            // Find event details (case-insensitive and trim comparison)
            var eventDetails = programmes
                .FirstOrDefault(p => string.Equals(p.PSCName.Trim(), eventName, StringComparison.OrdinalIgnoreCase));

            Debug.WriteLine("Event Details: " + (eventDetails != null ? "Found" : "Not Found"));

            if (eventDetails != null)
            {
                ViewBag.StartDateTime = (eventDetails.StartDate + eventDetails.StartTime)
                            .ToString("dddd dd MMM, hh:mm tt");

                ViewBag.EndDateTime = (eventDetails.EndDate + eventDetails.EndTime)
                                          .ToString("dddd dd MMM, hh:mm tt");
                ViewBag.Description = eventDetails.Description;
                ViewBag.Location = eventDetails.Location ?? "No location available.";
                ViewBag.Attire = eventDetails.Attire ?? "No attire information available.";
                ViewBag.NoOfSession = eventDetails.SessionDesc ?? "No session information available.";
                ViewBag.Type = eventDetails.Type ?? "No Type information available.";
            }
            else
            {
                ViewBag.Description = "No description available.";
            }
            ViewBag.fullname = HttpContext.Session.GetString("UserFullName");
            ViewBag.PSCNames = pscNames;
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
