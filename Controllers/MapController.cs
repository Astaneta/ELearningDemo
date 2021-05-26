using Microsoft.AspNetCore.Mvc;

namespace ElearningDemo.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Mappa";
            return View();
        }
    }
}