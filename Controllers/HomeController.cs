using Microsoft.AspNetCore.Mvc;

namespace ELearningFake.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}