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
