using Microsoft.AspNetCore.Mvc;

namespace ELearningFake.Controllers
{
    public class CorsiController : Controller
    {
        public IActionResult Index()
        {
            return Content("Sono Index");
        }

        public IActionResult Detail(string id)
        {
            return Content($"Sono Detail. Ho ricevuto l'id: {id}");
        }
    }
}