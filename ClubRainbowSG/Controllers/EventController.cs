using Microsoft.AspNetCore.Mvc;

namespace ClubRainbowSG.Controllers
{
    public class EventController : Controller
    {
        public IActionResult myevent()
        {
            return View();
        }
    }
}
