using clubrainbowSG.Data;
using Microsoft.AspNetCore.Mvc;

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
                               reg.ticket_count      // Ticket count from Registration table
                           }).ToList();
            ViewBag.myevents=myevents;

            return View();
        }
        public IActionResult cancelevent()
        {
            return View();
        }

        public IActionResult ticket()
        {
            return View();
        }
    }
}
