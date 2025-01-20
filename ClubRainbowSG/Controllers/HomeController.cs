using System.Diagnostics;
using clubrainbowSG.Data;
using ClubRainbowSG.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
            
          
            var programmes = _context.TestProgram.ToList();

            // Ensure session values are not null
            ViewBag.fullname = HttpContext.Session.GetString("UserFullName") ?? "Guest";
            
            if (programmes == null || !programmes.Any())
            {
                Debug.WriteLine("No programmes found in the database.");
                return View();
            }

            var pscNames = programmes.Select(p => p.pcsname).ToList();
            Debug.WriteLine("Available PSCNames: " + string.Join(", ", pscNames));

            string eventName = string.IsNullOrEmpty(pcsDropdown) ? "" : pcsDropdown.Trim();
            Debug.WriteLine("pcsDropdown Value: " + eventName);

            var eventDetails = programmes
                .FirstOrDefault(p => string.Equals(p.pcsname.Trim(), eventName, StringComparison.OrdinalIgnoreCase));

            Debug.WriteLine("Event Details: " + (eventDetails != null ? "Found" : "Not Found"));

            if (eventDetails != null)
            {
                ViewBag.StartDateTime = eventDetails.start_date_time.ToString("dddd dd MMM, hh:mm tt");
                ViewBag.EndDateTime = eventDetails.end_date_time.ToString("dddd dd MMM, hh:mm tt");
                ViewBag.Description = eventDetails.description;
                ViewBag.Location = eventDetails.location ?? "No location available.";
                ViewBag.Attire = eventDetails.attire ?? "No attire information available.";
                ViewBag.Type = eventDetails.type ?? "No Type information available.";
                ViewBag.pcscode = eventDetails.pcscode ?? "No Type information available.";
                ViewBag.Useraccountname = HttpContext.Session.GetString("Useraccountname") ?? "N/A";
                

                TempData["EventDetails"] = eventDetails.pcsname;
                TempData["Pcscode"] = eventDetails.pcscode ?? "No Type information available.";
                TempData["Useraccountname"] = HttpContext.Session.GetString("Useraccountname") ?? "N/A";
                TempData["session_name"] = eventDetails.session_name;

            }
            else
            {
                ViewBag.Description = "No description available.";
            }

            ViewBag.PSCNames = pscNames;
            ViewBag.EventName = eventName;

            return View();
        }
        [HttpPost]
        public IActionResult SaveTicketCount(int ticketCount)
        {
            Debug.WriteLine($"Received ticketCount: {ticketCount}");
            HttpContext.Session.SetInt32("TicketCount", ticketCount);

            int storedTicketCount = HttpContext.Session.GetInt32("TicketCount") ?? 0;
            Debug.WriteLine($"Stored TicketCount in Session: {storedTicketCount}");

            return Json(new { success = true });
        }
        [HttpGet]
        public IActionResult RegistrationDetails()
        {
            
            ViewBag.Pcscode = TempData["Pcscode"];
            ViewBag.Useraccountname = TempData["Useraccountname"];
            ViewBag.session_name = TempData["session_name"];
            ViewBag.ticketcount = HttpContext.Session.GetInt32("TicketCount");
            ViewBag.Userg1 = HttpContext.Session.GetString("Userg1") ?? "N/A";
            ViewBag.Userg2 = HttpContext.Session.GetString("Userg2") ?? "N/A";
            ViewBag.Userg3 = HttpContext.Session.GetString("Userg3") ?? "N/A";
            ViewBag.Userg4 = HttpContext.Session.GetString("Userg4") ?? "N/A";
            TempData.Keep("Pcscode");
            TempData.Keep("Useraccountname");
            TempData.Keep("session_name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationDetails([FromForm] RegistrationDto registrationDto)
        {
            try
            {
                var existingRegistration = await _context.Registration
                    .FirstOrDefaultAsync(r =>
                    r.contactFK == registrationDto.ContactFK &&
                    r.programmePCS_FK == registrationDto.programmePCS_FK &&
                    r.programmeSession_name_FK == registrationDto.programmeSession_name_FK);
                var testProgram = await _context.TestProgram
            .FirstOrDefaultAsync(tp => tp.pcscode == registrationDto.programmePCS_FK);
                if (existingRegistration != null&&existingRegistration.Status!= "Cancelled")
                {
                    ViewBag.ErrorMessage = "Duplicate registration is not allowed.";
                    ViewBag.Pcscode = registrationDto.programmePCS_FK;
                    ViewBag.Useraccountname = HttpContext.Session.GetString("Useraccountname");
                    ViewBag.session_name = testProgram.session_name;
                    ViewBag.ticketcount = HttpContext.Session.GetInt32("TicketCount");
                    ViewBag.Userg1 = HttpContext.Session.GetString("Userg1") ?? "N/A";
                    ViewBag.Userg2 = HttpContext.Session.GetString("Userg2") ?? "N/A";
                    ViewBag.Userg3 = HttpContext.Session.GetString("Userg3") ?? "N/A";
                    ViewBag.Userg4 = HttpContext.Session.GetString("Userg4") ?? "N/A";
                    // Return a view, error message, or simply redirect
                    ModelState.AddModelError(string.Empty, "Duplicate registration is not allowed.");
                    return View("registrationdetails"); // Replace with appropriate error handling
                }else if (existingRegistration != null && existingRegistration.Status == "Cancelled")
                {
                    existingRegistration.Status = "Pending";
                    existingRegistration.ticket_count = registrationDto.TicketCount;
                    await _context.SaveChangesAsync();
                    Console.WriteLine("changing status");
                    return RedirectToAction("myevent", "Event");
                }
                var registration = new Registration
                {
                    contactFK = registrationDto.ContactFK,
                    programmePCS_FK = registrationDto.programmePCS_FK,
                    programmeSession_name_FK = registrationDto.programmeSession_name_FK,
                    ticket_count = registrationDto.TicketCount
                };

                _context.Registration.Add(registration);
                await _context.SaveChangesAsync();

                return RedirectToAction("myevent", "Event");
            }
            catch (DbUpdateException ex)
            {
                var errorDetails = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, new
                {
                    success = false,
                    error = "Database update failed. Details: " + errorDetails
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    error = "An unexpected error occurred: " + ex.Message
                });
            }
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
