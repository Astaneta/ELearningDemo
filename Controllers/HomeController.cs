using System.Collections.Generic;
using System.Threading.Tasks;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.Services.Application;
using Microsoft.AspNetCore.Mvc;

namespace ELearningDemo.Controllers
{
    // L'attributo avrebbe effetto su tutte le action del controller
    //[ResponseCache (CacheProfileName = "Home")]
    public class HomeController : Controller
    {
        [ResponseCache (CacheProfileName = "Home")] // L'output html prodotto da questa action può essere messo in cache. La duration si misura in secondi
        public async Task<IActionResult> Index([FromServices] ICachedCorsoService corsoService) //FromServices istruisce il controller a cercare l'istanza nei services, per una corretta dependency injection
        {
            HomeListViewModel homeListViewModel = new HomeListViewModel
            {
                BestCourses = await corsoService.GetBestCourseAsync(),
                MostRecentCourses = await corsoService.GetMostRecentCourseAsync()
            };
            ViewData["Titolo"] = "E-Learning Demo";
            return View(homeListViewModel);
        }
    }
}