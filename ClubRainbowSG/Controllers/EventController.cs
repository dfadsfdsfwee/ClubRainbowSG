using clubrainbowSG.Data;
using Microsoft.AspNetCore.Mvc;
using ClubRainbowSG.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;
using System;

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

         
            var attendanceRecord = _context.Attendance
                                            .FirstOrDefault(a => a.contactFK == accountName && a.programmePCS_FK == programmePCS_FK);

            if (attendanceRecord != null)
            {
                
                if (attendanceRecord.Attendence == "Present")
                {
                    TempData["Message"] = "You have already marked your attendance for this event.";
                }
                else
                {
                   
                    attendanceRecord.Attendence = "Present";
                    _context.SaveChanges();
                    TempData["Message"] = "Attendance marked successfully!";
                }
            }
            else
            {
               
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


            return RedirectToAction("MyEvent");
        }

        public IActionResult cancelevent(string pcscode,string sesname)
        {
            ViewBag.pcscode = pcscode;
            ViewBag.accountname = HttpContext.Session.GetString("Useraccountname");
            ViewBag.sessionname = sesname;
			return View();
        }
        [HttpPost]
        public async Task<IActionResult> cancelevent(string pcscode, string accntname,string sesname)
        {
            if (string.IsNullOrEmpty(pcscode) || string.IsNullOrEmpty(accntname) || string.IsNullOrEmpty(sesname))
            {
                Console.WriteLine("nullinput");
                return RedirectToAction("myevent","Event");
            }
            
           
            var targetevent= await _context.Registration.FirstOrDefaultAsync(r => r.programmePCS_FK == pcscode&&r.contactFK== accntname && r.programmeSession_name_FK == sesname);
            var eventinfo = await _context.TestProgram.FirstOrDefaultAsync(tp=>tp.pcscode==pcscode && tp.session_name==sesname);
            var userinfo= await _context.Contacts.FirstOrDefaultAsync(c=>c.account_name==accntname);
            if (targetevent == null)
            {
                Console.WriteLine("nullvalue");
                return RedirectToAction("myevent", "Event");
            }
            var username = userinfo.full_name;
            var eventname = eventinfo.pcsname;
            var session = eventinfo.session_name;
            Console.WriteLine(username+" "+eventname+session);
            targetevent.Status = "Cancelled";
            await _context.SaveChangesAsync();
            /*var client = new HttpClient();
            var url = "https://prod-04.southeastasia.logic.azure.com:443/workflows/cda35aa3a3f243348fcd22b35a3944ff/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=8Br4x1XzwU265q3riTers3dzxHiVjUePt2bu8pJs-jY";

            var payload = new
            {
                username = userinfo.full_name,
                eventname = eventinfo.pcsname,
                datetime=eventinfo.session_name
            };
            var jsonString = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);*/
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
