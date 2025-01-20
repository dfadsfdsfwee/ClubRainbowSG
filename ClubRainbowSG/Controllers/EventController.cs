using clubrainbowSG.Data;
using Microsoft.AspNetCore.Mvc;
using ClubRainbowSG.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

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
                           && reg.Status != "Cancelled"
                           select new
                           {
                               evt.pcsname,       // Event name from Events table
                               evt.start_date_time,       // Start date from Events table
                               evt.end_date_time,         // End date from Events table
                               evt.location,           // Venue from Events table
                               reg.ticket_count      // Ticket count from Registration table
                           }).ToList();
            ViewBag.myevents=myevents;

            return View();
        }
        [HttpGet]
        public IActionResult cancelevent(string pcscode)
        {
            ViewBag.pcscode = pcscode;
            ViewBag.accountname = HttpContext.Session.GetString("Useraccountname");
			return View();
        }
        [HttpPost]
        public async Task<IActionResult> cancelevent(string pcscode, string accntname)
        {
            if (string.IsNullOrEmpty(pcscode) || string.IsNullOrEmpty(accntname))
            {
                Console.WriteLine("nullinput");
                return RedirectToAction("myevent","Event");
            }
            Console.WriteLine(pcscode);
            var targetevent= await _context.Registration.FirstOrDefaultAsync(r => r.programmePCS_FK == pcscode&&r.contactFK== accntname);
            if (targetevent == null)
            {
                Console.WriteLine("nullvalue");
                return RedirectToAction("myevent", "Event");
            }
            targetevent.Status = "Cancelled";
            await _context.SaveChangesAsync();
            Console.WriteLine("yay");
            return RedirectToAction("myevent","Event");
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
