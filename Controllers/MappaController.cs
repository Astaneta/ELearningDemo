using Microsoft.AspNetCore.Mvc;

namespace ElearningDemo.Controllers
{
    public class MappaController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Mappa";
            return View();
        }
    }
}