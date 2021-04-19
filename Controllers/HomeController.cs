using Microsoft.AspNetCore.Mvc;

namespace ELearningDemo.Controllers
{
    // L'attributo avrebbe effetto su tutte le action del controller
    //[ResponseCache (CacheProfileName = "Home")]
    public class HomeController : Controller
    {
        [ResponseCache (CacheProfileName = "Home")] // L'output html prodotto da questa action può essere messo in cache. La duration si misura in secondi
        public IActionResult Index()
        {
            ViewData["Titolo"] = "E-Learning Demo";
            return View();
        }
    }
}