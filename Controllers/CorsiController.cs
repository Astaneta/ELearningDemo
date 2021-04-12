using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elearningfake.Models.Services.Application;
using Elearningfake.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ELearningfake.Controllers
{
    public class CorsiController : Controller
    {
        private readonly ICorsoService corsiService;

        public CorsiController(ICorsoService corsiService)
        {
            this.corsiService = corsiService;
        }
        
        public async Task<IActionResult> Index()
        {
            List<CorsiViewModel> corsi = await corsiService.GetCorsiAsync();
            ViewData["Titolo"] = "Catalogo corsi";
            return View(corsi);
        }

        public async Task<IActionResult> Detail(int id)
        {
            CorsoDetailViewModel corso = await corsiService.GetCorsoAsync(id);
            ViewData["Titolo"] = corso.Title;
            return View(corso);
        }
    }
}