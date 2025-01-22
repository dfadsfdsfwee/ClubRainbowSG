using clubrainbowSG.Data;
using Microsoft.AspNetCore.Mvc;
using ClubRainbowSG.Models;
using System.Diagnostics;
namespace ClubRainbowSG.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult myevent()
        {
            var accountname = HttpContext.Session.GetString("Useraccountname");
            var myevents= (from reg in _context.Registration
                           join evt in _context.TestProgram
                           on reg.programmePCS_FK equals evt.pcscode // Join condition
                           where reg.contactFK == accountname // Filter by contactfk
                           select new
                           {
                               evt.pcsname,       // Event name from Events table
                               evt.start_date_time,       // Start date from Events table
                               evt.end_date_time,         // End date from Events table
                               evt.location,           // Venue from Events table
                               reg.ticket_count,      // Ticket count from Registration table
                               reg.programmePCS_FK
                           }).ToList();
            ViewBag.myevents=myevents;

            return View();
        }

        [HttpPost]
        public IActionResult MarkAttendance(string programmePCS_FK, int ticket_count)
        {
            var accountName = HttpContext.Session.GetString("Useraccountname");

            var sessionName = _context.TestProgram
                                .Where(e => e.pcscode == programmePCS_FK)
                                .Select(e => e.pcsname) 
                                .FirstOrDefault();

            // Check if the record already exists in the Attendance table
            var attendanceRecord = _context.Attendance
                                            .FirstOrDefault(a => a.contactFK == accountName && a.programmePCS_FK == programmePCS_FK);

            if (attendanceRecord != null)
            {
                // If the attendance is already marked, return a message (optional)
                if (attendanceRecord.Attendence == "Present")
                {
                    TempData["Message"] = "You have already marked your attendance for this event.";
                }
                else
                {
                    // Update attendance if not already marked
                    attendanceRecord.Attendence = "Present";
                    _context.SaveChanges();
                    TempData["Message"] = "Attendance marked successfully!";
                }
            }
            else
            {
                // If no record exists, create a new entry for the attendance
                var newAttendance = new Attendance
                {
                    contactFK = accountName,
                    programmePCS_FK = programmePCS_FK,
                    
                    Attendence = "Present",
                    ticket_count = ticket_count,
                    programmeSession_nameFK = sessionName,
            };
                _context.Attendance.Add(newAttendance);
                _context.SaveChanges();
                TempData["Message"] = "Attendance marked successfully!";
            }

            // After marking attendance, redirect back to the event list page
            return RedirectToAction("MyEvent");
        }

        public IActionResult cancelevent()
        {
            return View();
        }

        public IActionResult ticket()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Qrcode()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ScanResult([FromBody] QRCodeData data)
        {
            Console.WriteLine($"Scanned Text: {data.ScannedText}");
            return Ok();
        }

    }
}
