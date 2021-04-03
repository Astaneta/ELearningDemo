using Microsoft.AspNetCore.Mvc;

namespace ELearningfake.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Titolo"] = "E-Learning Fake";
            return View();
        }
    }
}