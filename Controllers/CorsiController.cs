using Microsoft.AspNetCore.Mvc;

namespace ELearningFake.Controllers
{
    public class CorsiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(string id)
        {
            return View();
        }
    }
}