using Microsoft.AspNetCore.Mvc;

namespace ElearningDemo.Controllers
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