using Microsoft.AspNetCore.Mvc;

namespace Elearningfake.Controllers
{
    public class MappaController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Titolo"] = "Mappa";
            return View();
        }
    }
}